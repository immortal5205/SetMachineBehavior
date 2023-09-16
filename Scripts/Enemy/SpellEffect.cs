using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpellEffect : StateMachineBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    public float speed;
    public float spellTime;
    private float i;


     override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player =  GameObject.FindGameObjectWithTag("Player").transform;
       rb = animator.GetComponent<Rigidbody2D>();

                   
    }
     override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        i -=Time.deltaTime;
     
        Vector2 target = new Vector2(player.position.x , rb.position.y);
        Vector2 newPos =  Vector2.MoveTowards(rb.position, target , speed*Time.deltaTime);

        rb.MovePosition(newPos);

        if(i <= 0 )
        {
            animator.SetTrigger("attack");
            i = spellTime;
        }
       
    }

    

   

    
}
