using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalk : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    Death death;
    Attack attack;
    public float speed;
    private float attackRateCounter = 0;
    public float attackTime;
    private float i;
  
    

 
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player =  GameObject.FindGameObjectWithTag("Player").transform;
       
       rb = animator.GetComponent<Rigidbody2D>();
        death = animator.GetComponent<Death>();
       attack = animator.GetComponent<Attack>();
     

    
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        death.FollowPlayer();

       Vector2 target = new Vector2(player.position.x ,rb.position.y);

         
        Vector2 newPos =  Vector2.MoveTowards(rb.position, target , speed*Time.deltaTime);

        rb.MovePosition(newPos);
        attackRateCounter -= Time.deltaTime;
        i -= Time.deltaTime;

       if (Vector2.Distance(player.position , death.transform.position) <= attack.attackRange)
       {
            if(attackRateCounter <=0)
            {
            //攻击
            animator.SetTrigger("attack");
            attackRateCounter = attack.attactRate;

            }
       }

       if(i <=0)
       {
            animator.SetBool("spell",true);
            i = attackTime;
           
       }
       else
       {
            animator.SetBool("spell" , false);
       }

       
    }

 
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack");
    }

  
}
