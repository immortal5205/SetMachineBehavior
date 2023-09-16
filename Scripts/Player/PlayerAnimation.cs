using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private PlayerController playerController;
   
    private void Awake() 
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerController = GetComponent<PlayerController>();
       
    }

    private void Update() 
    {
        SwitchAnimation();
        
    }

    public void SwitchAnimation()
    {
       
     
        animator.SetFloat("velocityX",MathF.Abs(rb.velocity.x));
        
        animator.SetFloat("velocityY",rb.velocity.y);   
        animator.SetBool("isGround" , physicsCheck.isGround);
        animator.SetBool("isDead", playerController.isDead);
        animator.SetBool("isAttack" , playerController.isAttack);
       
    }

    public void Hurt()
    {
       animator.SetTrigger("hurt");
    }

    public void PlayerAttack()
    {
        animator.SetTrigger("attack");
       
    }
}
