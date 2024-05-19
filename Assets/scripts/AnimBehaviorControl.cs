using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimBehaviorControl : MonoBehaviour
{
    public Animator animator;

    public float dir;
    public bool isJumping;
    public bool isDead;

    [SerializeField]
    public float m_timeSinceAttack = 0f;
    [SerializeField]
    public int m_currentAttack = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_timeSinceAttack += Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RightAnimation()
    {
        animator.SetFloat("speed", Mathf.Abs(dir));
    }
    public void JumpAnimation()
    {
        animator.SetBool("jumping", isJumping);
    }
    public void LeftClick()
    {

        m_currentAttack++;
        if (m_currentAttack > 0.25f)
        {
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            animator.SetTrigger("BasicAttack" + m_currentAttack);
            // Reset timer
            m_timeSinceAttack = 0.0f;
        }
    }
    public void LeftClickWhileMoving()
    {

        animator.SetTrigger("Attack1");
    }

    public void DeathAnimation()
    {
        animator.SetTrigger("isDead");
    }
    public void HitAniamtion()
    {
        animator.SetTrigger("isHit");
    }
}
