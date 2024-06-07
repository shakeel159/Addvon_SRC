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
    public BossHealthMonitor healthBar;
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
    public Transform attackPointFrontMalee;
    public GameObject player;
    public float attackDistance;
    public float attackRanf;
    public float attackRanTwo;
    private Vector2 knockBack;

    private bool attackStarted;
    public bool flip;
    private bool dashAttack;
    private bool closeAttack;
    bool canUseDash;
    // Start is called before the first frame update
    void Start()
    {
        anime.isDead = false;
        behaviour_State = BehaviourState.Idle;
        anime = GetComponent<bossAnimator>();
        EnemyRb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        healthBar.MaxHealth(currentHealth);
        knockBack = new Vector2(3f, 0.0f);
        attackRanf = 2.2f;
        attackRanTwo = 1.4f;
        canUseDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(cC.bossFightStart == true)
        {
            attackStarted = true;
            bossHealthUI.SetActive(true);
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
                if (attackStarted == true)
                {
                    DelayIdle();
                    //StartCoroutine(DelayChasing());
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
                healthBar.SetHealth(currentHealth);
                //StartCoroutine(DelayIdle());
                behaviour_State = BehaviourState.Idle;
                break;
            case BehaviourState.Attacking:
                speed = 0f;
                coll.isTrigger = true;
                StartCoroutine(DelayAttack());
                break;
            case BehaviourState.AttckingTwo:
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
        }
        if (canUseDash == false)
        {
            StartCoroutine(delayDashUsage());
        }
        DeathSequence();
    }
    IEnumerator delayDashUsage()
    {
        yield return new WaitForSeconds(6f);
        canUseDash = true;
    }
    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
    private void AttackAnim()// change state to attack and play attack animation when close to player
    {
        if (Vector2.Distance(transform.position, player.transform.position) < attackRanf) //AND is swinging sword;
        {
            if (canUseDash == true)
            {
                dashAttack = true;
                Debug.Log("DASH COLLISION IN PLAY");
                behaviour_State = BehaviourState.Attacking; //Temporarly play normal attack // future randomize attack from 1 to 3
                canUseDash = false;
            }

        }

        if (Vector2.Distance(transform.position, player.transform.position) < attackRanTwo)
        {
            //coll.isTrigger = true;
            if (canUseDash == false)
            {
                closeAttack = true;
                Debug.Log("Mallee COLLISION IN PLAY");
                behaviour_State = BehaviourState.AttckingTwo;
            }
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
    private void DelayIdle()
    {
        if (anime.isDead == false)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < attackRanTwo)
            {
                behaviour_State = BehaviourState.AttckingTwo;
            }
            else
            {
                behaviour_State = BehaviourState.Chasing;
            }

        }
    }
    private IEnumerator DelayChasing()
    {
        yield return new WaitForSeconds(1f);
        behaviour_State = BehaviourState.Chasing;
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
            if (dashAttack == true)
            {
                anime.AttackAnimation();
                yield return new WaitForSeconds(1.5f);
                anime.isSwinging = true;
                BasicAttack(dashDMG);
            }
            if(closeAttack == true)
            {
                anime.AttackMaleeAnimation();
                yield return new WaitForSeconds(1f);
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
            hitplayerFront = Physics2D.OverlapCircleAll(attackPointFrontMalee.position, attackRanf, playerLayer);
            closeAttack = false;
        }

        foreach (var playerObject in hitplayerFront)
        {
            if (playerObject.GetComponent<Player>().isHit == true)
            {
                //Invoke("returnNotHit", .4f);
                returnNotHit();
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gameObject.tag == "Player")
        //{
        //    if (behaviour_State != BehaviourState.Attacking)
        //    {
        //        closeAttack = true;
        //        Debug.Log("DASH COLLISION IN PLAY");
        //        behaviour_State = BehaviourState.AttckingTwo;
        //    }
        //    coll.isTrigger = true;
        //}
      
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //coll.isTrigger = false;
    }
    public void OnDrawGizmosSelected()
    {
        if (attackPointFront == null)
            return;
        Gizmos.DrawWireSphere(attackPointFront.position, attackRanf);
    }
}

