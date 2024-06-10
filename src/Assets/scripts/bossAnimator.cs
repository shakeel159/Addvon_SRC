using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class bossAnimator : MonoBehaviour
{
    public Animator animator;
    public bool isIdle;
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
    public void IdleAnimation()
    {
        animator.SetBool("isIdle", isIdle); // if isIdle is false run idle
    }
    public void walkAnimation()
    {
        animator.SetBool("isIdle", isIdle); // if isIdle is true run walk
    }
    public void AttackAnimation()
    {
        //isSwinging = true;
        animator.SetTrigger("attackOne");
        //isSwinging = false;
    }
    public void AttackMaleeAnimation()
    {
        animator.SetTrigger("attackTwo");
        //StartCoroutine(secondPartAniamtion());
        //isSwinging = false;
    }
    IEnumerator secondPartAniamtion()
    {
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("attaclTwoPartTwo");
    }
    public void StunnedAnimation()
    {
        animator.SetTrigger("stunned");

        Debug.Log("STUNN ANIMATION PLAY");
    }
    public void DeathAnimation()
    {
        animator.SetTrigger("isDeath");
        StartCoroutine(DestroyGameObject());
    }
    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(7f);
        Destroy(this.gameObject);
    }
}
