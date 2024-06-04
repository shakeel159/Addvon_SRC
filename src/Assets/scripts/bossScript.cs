using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static EnemyPotrol;
using static UnityEngine.UI.Image;

public class bossScript : Humans
{
    public enum BehaviourState { Idle, Chasing, Attacking, AttackingOne, AttckingTwo, Dash, stunned, Dead } // states to implement
    public BehaviourState behaviour_State;


    public GameObject bossHealthUI;
    public ChildCollision cC;

    private float speed;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    [SerializeField] private bossAnimator anime;
    [SerializeField] protected Rigidbody2D EnemyRb;
    [SerializeField] protected BoxCollider2D coll;
    Collider2D[] hitplayerFront;
    public LayerMask playerLayer;
    public Transform attackPointFront;
    public GameObject player;
    public float attackDistance;
    public float attackRanf;
    public float attackRanTwo;
    private Vector2 knockBack;

    private bool attackStarted;
    public bool flip;
    private bool dashAttack;
    private bool closeAttack;
    private float rechargDash;
    private float rechargeMalee;

    public Slider healthBar;
    // Start is called before the first frame update
    void Start()
    {
        anime.isDead = false;
        behaviour_State = BehaviourState.Idle;
        anime = GetComponent<bossAnimator>();
        EnemyRb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        healthBar.maxValue = currentHealth;
        healthBar.value = currentHealth;
        knockBack = new Vector2(3f, 0.0f);
        attackRanf = 1.4f;
        attackRanTwo = 0.6f;
        rechargDash = 10f;
        rechargeMalee = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(cC.bossFightStart == true)
        {
            attackStarted = true;
            bossHealthUI.SetActive(true);
            currentHealth = healthBar.value;
            behaviour_State = BehaviourState.Chasing;
            cC.bossFightStart = false;
        }
        switch(behaviour_State)
        {
            case BehaviourState.Idle:
                anime.isIdle = true;
                anime.IdleAnimation();
                Speed = 0f;
                //if(attackStarted == true) => wait#secForStateSwitch;
                if(attackStarted == true)
                {
                    StartCoroutine(DelayIdle());
                }
                break;
            case BehaviourState.Chasing:
                anime.isIdle = false;
                speed = 1f;
                anime.IdleAnimation();
                if (IsFacingRight())
                {
                    EnemyRb.velocity = new Vector2(-Speed, 0);
                }
                else
                {
                    EnemyRb.velocity = new Vector2(Speed, 0);
                }
                flipCharacterDiractionTowardsPlayer();
                break;
            case BehaviourState.stunned:
                speed = 0f;
                anime.isSwinging = false;
                EnemyRb.AddForce(knockBack, ForceMode2D.Impulse);
                anime.StunnedAnimation();
                StartCoroutine(DelayIdle());
                break;
            case BehaviourState.Attacking:
                dashAttack = true;
                speed = 0f;
                StartCoroutine(DelayAttack());
                break;
            case BehaviourState.AttckingTwo:
                closeAttack = true;
                speed = 0f;
                StartCoroutine(DelayAttack());
                break;
            case BehaviourState.Dead:
                anime.isIdle = false;
                anime.isDead = true;
                speed = 0f;
                EnemyRb.velocity = new Vector2(0,0); 
                anime.DeathAnimation();              
                break;

        }
        if(anime.isDead == false)
        {
            AttackAnim();
            RechargeDahs();
        }
        DeathSequence();
    }
    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(7f);
        Destroy(this.gameObject);
    }
    private void AttackAnim()// change state to attack and play attack animation when close to player
    {
        if (Vector2.Distance(transform.position, player.transform.position) < attackRanf && rechargDash > 0) //AND is swinging sword;
        {
            behaviour_State = BehaviourState.Attacking; //Temporarly play normal attack // future randomize attack from 1 to 3
            rechargDash = 0;
        }
        if (Vector2.Distance(transform.position, player.transform.position) < attackRanTwo && rechargeMalee > 0) //AND is swinging sword;
        {
            behaviour_State = BehaviourState.AttckingTwo; //Temporarly play normal attack // future randomize attack from 1 to 3
            rechargeMalee = 0;
        }

    }
    void RechargeDahs()
    {
        if(rechargDash != 10)
        {
            rechargDash += Time.deltaTime;
        }
        if(rechargeMalee != 5f)
        {
            rechargeMalee += Time.deltaTime;
        }
    }
    private bool IsFacingRight()//Checks if object is facing right and send sback bool if true else false
    {
        return transform.localScale.x > Mathf.Epsilon;
    }
    void flipCharacterDiractionTowardsPlayer()//flip diraction of object towards where its going// in this case towards player
    {
        UnityEngine.Vector2 scale = transform.localScale;
        if (player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? 1 : -1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? 1 : -1);
        }

        transform.localScale = scale;
    }
    private IEnumerator DelayIdle()
    {
        if (anime.isDead == false)
        {
           
            yield return new WaitForSeconds(2f);
            if (Vector2.Distance(transform.position, player.transform.position) < attackRanTwo)
            {
                behaviour_State = BehaviourState.AttackingOne;
            }
            else
            {
                behaviour_State = BehaviourState.Chasing;
            }

        }
    }
    private void DeathSequence()
    {
        if (playerState == PlayerState.Dead)
        {
            anime.isDead = true;
            behaviour_State = BehaviourState.Dead;
        }
    }
    IEnumerator PauseAnimation()//pause object in idle sequance 
    {
        yield return new WaitForSeconds(2f);
        if (transform.localScale.x < 0)
        {
            transform.localScale = new Vector2(2f, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(-Mathf.Sign(EnemyRb.velocity.x) * 2f, transform.localScale.y);
        }
        behaviour_State = BehaviourState.Chasing;
    }
    public IEnumerator DelayAttack()
    {
        float dashDMG = attackDmg;
        float maleeDMG = attackDmg - 5;
        if (anime.isDead == false)
        {
            anime.AttackAnimation();
            if (dashAttack == true)
            {
                yield return new WaitForSeconds(2f);
                anime.isSwinging = true;
                BasicAttack(dashDMG);
            }
            if(closeAttack == true)
            {
                yield return new WaitForSeconds(.8f);
                anime.isSwinging = true;
                BasicAttack(maleeDMG);
            }
        }
        //StartCoroutine(DelayIdle());
        behaviour_State = BehaviourState.Idle;
    }
    public override void BasicAttack(float dmg)
    {
        if(dashAttack == true)
        {
            hitplayerFront = Physics2D.OverlapCircleAll(attackPointFront.position, attackRanf, playerLayer);
            dashAttack = false;
        }
        if(closeAttack == true)
        {
            hitplayerFront = Physics2D.OverlapCircleAll(attackPointFront.position, attackRanTwo, playerLayer);
            closeAttack = false;
        }
       

        foreach (var playerObject in hitplayerFront)
        {
            if (playerObject.GetComponent<Player>().isHit == true)
            {
                Invoke("returnNotHit", .4f);
            }
            else if (playerObject.GetComponent<Player>().isHit == false && anime.isSwinging == true)
            {
                playerObject.GetComponent<Player>().DmgTaken(dmg);
            }
        }
    }
    private void returnNotHit()
    {
        player.GetComponent<Player>().isHit = false;
    }
    public override void DmgTaken(float damage)
    {
        base.DmgTaken(damage);
        isHit = true;
        behaviour_State = BehaviourState.stunned;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.gameObject.name);
    }
    // This will be called when the collider exits a trigger collider
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger exited by: " + other.gameObject.name);
    }
    public void OnDrawGizmosSelected()
    {
        if (attackPointFront == null)
            return;
        Gizmos.DrawWireSphere(attackPointFront.position, attackRanf);
    }
}

