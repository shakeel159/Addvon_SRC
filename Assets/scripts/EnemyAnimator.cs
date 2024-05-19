using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Animator animator;

    public bool isRunning;
    public bool isDead;
    public bool isSwinging;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RunAnimation()
    {
        //animator.SetFloat("speed", Mathf.Abs(dir));
        animator.SetBool("isRunning", isRunning);
    }
    public void IdleAnimation()
    {
        //animator.SetFloat("speed", 0);
        animator.SetBool("isRunning", isRunning);
        //animator.SetBool("isAttacking", isAttacking);
    }
    public void AttackAnimation()
    {
        //isSwinging = true;
        animator.SetTrigger("enemyAttack");
    }
    public void DeathAnimation()
    {
        animator.SetTrigger("isDeath");
    }
    public void StunnedAnimation()
    {
        animator.SetTrigger("StunnedAnimation");
    }
}
