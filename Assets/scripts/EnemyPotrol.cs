using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using static EnemyPotrol;

public class EnemyPotrol : Humans
{
    public enum EnemyState { Idle, Patrolling, chasing, Attack, Stunned, Dead } // states to implement
    public EnemyState enemyState;

    private float speed;
    public float Speed { 
        get { return speed; }
        set { speed = value; }
    }
    [SerializeField] private EnemyAnimator anime;
    [SerializeField] protected Rigidbody2D EnemyRb;
    [SerializeField] protected BoxCollider2D coll;
    public Transform attackPointFront;
    Collider2D[] hitplayerFront;
    public GameObject player;
    public Transform[] patrolPoints;
    public LayerMask playerLayer;
    private Vector2 knockBack;

    public bool flip;
    public float chaseDistance;
    public float attackDistance;
    public int currentPatrolPoint = 0;
    public float attackRanf = .6f;


    // Start is called before the first frame update
    void Start()
    {
        anime.isDead = false;
        enemyState = EnemyState.Patrolling;

        //enemies = GetComponent<Enemies>();
        anime = GetComponent<EnemyAnimator>();
        EnemyRb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

        knockBack = new Vector2(3f, 0.0f);

    }
    // Update is called once per frame
    void Update()
    {

        switch (enemyState)
        {
            case EnemyState.Idle:
                anime.isRunning = false;
                anime.IdleAnimation();
                Speed = 0f;
                break;
            case EnemyState.Patrolling:
                anime.isRunning = true;
                anime.RunAnimation();
                GoTowardsEdge();
                Speed = .5f;
                break;
            case EnemyState.chasing:
                Speed = 1f;
                anime.RunAnimation();
                flipCharacterDiractionTowardsPlayer();
                break;
            case EnemyState.Attack:     
                speed = 0f;
                StartCoroutine(DelayAttack());
                StartCoroutine(DelayIdle());
                break;
            case EnemyState.Stunned:
                speed = 0f;
                EnemyRb.AddForce(knockBack, ForceMode2D.Impulse);
                anime.StunnedAnimation();
                StartCoroutine(DelayIdle());
                break;
            case EnemyState.Dead:
                anime.isRunning = false;
                speed = 0f;
                anime.DeathAnimation();
                coll.size = new Vector2(0.35f, 0.001f);
                //coll.size = new Vector2(coll.size.x, coll.size.y/2);
                //Vector2 newPosition = this.transform.position;
                //newPosition.y = -.5f;
                //transform.position = newPosition;
                break;              
        }
        if(anime.isDead == false)
        {
            ChangeDirAndState();
            AttackAnim();

            if (currentPatrolPoint >= patrolPoints.Length)
            {
                currentPatrolPoint = 0;
            }


            if (IsFacingRight())
            {
                EnemyRb.velocity = new Vector2(Speed, 0);
            }
            else
            {
                EnemyRb.velocity = new Vector2(-Speed, 0);
            }
        }
        DeathSequence();
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    private IEnumerator DelayIdle()
    {
        if(anime.isDead == false)
        {
            yield return new WaitForSeconds(.3f);
            enemyState = EnemyState.Idle;
        }
    }
    private bool IsFacingRight()//Checks if object is facing right and send sback bool if true else false
    {
        return transform.localScale.x > Mathf.Epsilon;
    }
    private void GoTowardsEdge()//when in patrol state object goes to position of potrol points
    {
        Vector2 playerLoc = transform.position;
        Vector2 moveToPoint = patrolPoints[currentPatrolPoint].position;
        transform.position = Vector2.MoveTowards(transform.position, moveToPoint, Speed * Time.deltaTime);
        if (playerLoc == moveToPoint)
        {
            currentPatrolPoint++;

        }
    }
    private void ChangeDirAndState()//when player in close proximity change state of object and direction
    {


        if(Vector2.Distance(transform.position, player.transform.position) < chaseDistance)
        {
            enemyState = EnemyState.chasing;
            if(enemyState == EnemyState.chasing && Vector2.Distance(transform.position, player.transform.position) > chaseDistance)
            {
                enemyState = EnemyState.Patrolling;
            }
        }

    }
    private void AttackAnim()// change state to attack and play attack animation when close to player
    {
        if(Vector2.Distance(transform.position, player.transform.position) < attackDistance) //AND is swinging sword;
        {
            enemyState = EnemyState.Attack;
        }

    }
    private void DeathSequence()
    {
        if (playerState == PlayerState.Dead)
        {
            anime.isDead = true;
            enemyState = EnemyState.Dead;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "walls")
        {
            currentPatrolPoint++;
            enemyState = EnemyState.Idle;
            StartCoroutine(PauseAnimation());
        }
    }
    IEnumerator PauseAnimation()//pause object in idle sequance 
    {
        yield return new WaitForSeconds(2f);
        if(transform.localScale.x < 0)
        {
            transform.localScale = new Vector2(2f, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(-Mathf.Sign(EnemyRb.velocity.x) * 2f, transform.localScale.y);
        }
        enemyState = EnemyState.Patrolling;
    }
    void flipCharacterDiractionTowardsPlayer()//flip diraction of object towards where its going// in this case towards player
    {
        Vector2 scale = transform.localScale;
        if (player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
        }

        transform.localScale = scale;
    }
    public IEnumerator DelayAttack()
    {
        if(anime.isDead == false)
        {
            yield return new WaitForSeconds(.5f);
            BasicAttack(attackDmg);
            anime.AttackAnimation();
            anime.isSwinging = true;
        }
        else
        {
            anime.isSwinging = false;
        }

    }
    public override void BasicAttack(float dmg)   
    {
        hitplayerFront = Physics2D.OverlapCircleAll(attackPointFront.position, attackRanf, playerLayer);

        foreach (var playerObject in hitplayerFront)
        {
            if (playerObject.GetComponent<Player>().isHit == true)
            {
                Invoke("returnNotHit", .5f);
            }
            else if (playerObject.GetComponent<Player>().isHit == false && anime.isSwinging == true)
            {
                playerObject.GetComponent<Player>().DmgTaken(dmg);
                anime.isSwinging = false;
            }
        }
    }
    public override void DmgTaken(float damage)
    {
        base.DmgTaken(damage);
        isHit = true;
        enemyState = EnemyState.Stunned;
    }
    private void returnNotHit()
    {
        player.GetComponent<Player>().isHit = false;
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPointFront == null)
            return;
        Gizmos.DrawWireSphere(attackPointFront.position, attackRanf);
    }
}
