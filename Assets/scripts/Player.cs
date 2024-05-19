using Cainos.PixelArtPlatformer_VillageProps;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static EnemyPotrol;

public class Player : Humans
{
    private PlayerControl control;
    public AnimBehaviorControl anim;
    private SpriteRenderer playerSprite;
    [SerializeField] public GameObject GameOverMenuUI;
    [SerializeField] public GameObject MainMenuUI;

    //public Transform attackPoint;
    private float maxStamina;
    public Transform attackPointFront;
    public Transform attackPointBack;
    public float attackRanf = .6f;
    public LayerMask enemyyLayers;
    private bool isAttacking = false;
    private float maxHealth;
    //private float currentStamina;
    private Vector2 knockBack;



    public HealthBar healthBar;
    public StaminaBar staminaBar;
    Collider2D[] hitEnemiesFront;
    Collider2D[] hitEnemiesBack;
    CapsuleCollider2D playerCollider;
    public ProjectileBehaviour projectilePrefabe;
    public Transform launchOffset;
    public Inventoey inv;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<AnimBehaviorControl>();
        control = GetComponent<PlayerControl>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<CapsuleCollider2D>();

        StartStats();
    }
    public void StartStats()
    {
        Name = gameObject.name;
        maxHealth = 100;
        maxStamina = 50;
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        healthBar.SetMaxHealth(maxHealth);
        staminaBar.SetMaxStamina(maxStamina);
        staminaBar.SetStamina(maxStamina - 25f);
        attackDmg = 10;
        fireDmg = 35;
        knockBack = new Vector2(-3f, 0.0f);

        DisplayStats();
        anim.isDead = false;
        isHit = false;

        GameOverMenuUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        switch (control.actionState)
        {
            case PlayerControl.ActionState.Normal:
                if (anim.isDead == false)
                {
                    isAttacking = false;
                    anim.dir = control.horizontalInput;
                    MovementLeftRight();
                    anim.RightAnimation();
                    //if (control.isAttacking == true)
                    //{
                    //    Invoke("NoLongerAttacking", 1f);
                    //}
                }
                else if(anim.isDead == true) 
                {
                    anim.DeathAnimation();
                    playerCollider.size = new Vector2(0.35f, 0.001f); // change size collider for body to lay on floor and not float
                    control.enabled = false; // Disable player controls
                    GameOverMenuUI.SetActive(true);
                }
                break;
            case PlayerControl.ActionState.Jump:
                anim.isJumping = true;
                break;
            case PlayerControl.ActionState.Attack:
                anim.LeftClick();
                BasicAttack(attackDmg);
                break;
            case PlayerControl.ActionState.Walk_Attack:
                anim.LeftClickWhileMoving();
                BasicAttack(attackDmg);
                break;
            case PlayerControl.ActionState.special_Attack:
                if(currentStamina > 0)
                {
                    anim.LeftClick();
                    Instantiate(projectilePrefabe, launchOffset.position, launchOffset.transform.rotation);
                    currentStamina -= 25f;
                    staminaBar.SetStamina(currentStamina);
                }
                break;
            case PlayerControl.ActionState.Stunned:
                //PLAY STUNNED ANIMATION HER
                //Debug.Log("During STUNN");
                anim.HitAniamtion();
                control.inStun = false;
                //control.actionState = PlayerControl.ActionState.Normal;
                //StartCoroutine(changeStateToNormal());
                break;
        }
        if (playerState == PlayerState.Dead)
        {
            anim.isDead = true;
        }

        anim.JumpAnimation();
        control.InteractButton();

        useItemes();

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            control.isOnGround = true;
            anim.isJumping = false;
        }

    }
    public void useItemes()
    {
        if(control.isUsingItem == true)
        {
            if(inv.potionInInventory > 0)
            {
                staminaBar.SetStamina(25f);
                currentStamina += 50f;
                inv.potionInInventory--;
                inv.potionText.text = inv.potionInInventory.ToString();
            }   
        }
        else if (control.isUsingFood == true)
        {
            if (inv.appleInInventory > 0)
            {
                healthBar.SetHealth(maxHealth);
                currentHealth += maxHealth;
                inv.appleInInventory--;
                inv.appleText.text = inv.appleInInventory.ToString();
            }
        }
        control.isUsingItem = false;
    }
    public override void DmgTaken(float damage)
    {
        base.DmgTaken(damage);
        control.inStun = true;
        isHit = true;
        control.rb.AddForce(knockBack, ForceMode2D.Impulse);
        //control.horizontalInput = 0;
        //transform.localScale = new Vector2((Mathf.Sign(control.rb.velocity.x) * 2.25f), transform.localScale.y);
        healthBar.SetHealth(currentHealth);
        staminaBar.SetStamina(currentStamina);
        //Debug.Log("PRE STUNN");
        //control.actionState = PlayerControl.ActionState.Stunned;
    }
    private IEnumerator changeStateToNormal()
    {
        yield return new WaitForSeconds(2f);
        control.actionState = PlayerControl.ActionState.Normal;
    }
    private void MovementLeftRight()
    {
        if (control.horizontalInput <= -0.1)
        {
            transform.localScale = new Vector2(-(Mathf.Sign(control.rb.velocity.x) * 2.2f), transform.localScale.y);
            launchOffset.transform.Rotate(0f, -180f, 0f);
        }
        if (control.horizontalInput >= 0.1)
        {
            transform.localScale = new Vector2((Mathf.Sign(control.rb.velocity.x) * 2.25f), transform.localScale.y);
            //transform.Rotate(0f, 180f, 0f);
            launchOffset.transform.Rotate(0f, 180f, 0f);

        }
    }
    public override void BasicAttack(float dmg)     //RAPEATING CODE HERE CANT THINK OF SOLUTION AT MOOMENT  BESIDES GOING ONE ATTACKPOINT ROUTE
    {
        isAttacking = true;
        hitEnemiesFront = Physics2D.OverlapCircleAll(attackPointFront.position, attackRanf, enemyyLayers);
        hitEnemiesBack = Physics2D.OverlapCircleAll(attackPointBack.position, attackRanf, enemyyLayers);

        foreach (var enemy in hitEnemiesFront)
        {
            //Debug.Log("enemy hit");
            enemy.GetComponent<EnemyPotrol>().DmgTaken(attackDmg);

        }
        foreach (var enemy in hitEnemiesBack)
        {
            //Debug.Log("enemy hit");
            enemy.GetComponent<EnemyPotrol>().DmgTaken(attackDmg);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPointFront == null)
            return;
        Gizmos.DrawWireSphere(attackPointFront.position, attackRanf);
        //if (attackPointBack == null)
        //    return;
        //Gizmos.DrawWireSphere(attackPointBack.position, attackRanf);
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
