using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Death : MonoBehaviour
{
    private Transform player;
    private Vector3 target;
    private Vector3 moveDir;
    private Rigidbody2D rb;
    public Slider deathHPBar;
    private Character character;
    private Animator animator;

    private void Awake() 
    {
        character = GetComponent<Character>();
        animator = GetComponent<Animator>();
      
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        player = GameObject.FindGameObjectWithTag("Player").transform; 

    }

    private void Update() 
    {
        deathHPBar.value = character.currentHP;
      
    }

    public void FollowPlayer()
    {
        target = new Vector3(player.position.x, rb.transform.position.y, 0);
        moveDir = (target - rb.transform.position).normalized;

        if (moveDir.x > 0)
            rb.transform.localScale = new Vector3(-40, 40, 40);
        if (moveDir.x < 0)
            rb.transform.localScale = new Vector3(40, 40, 40);
    }


     public void OnDie()
    {
        gameObject.layer = 2;
        animator.SetBool("dead", true);
       
    }

    public void DestroyAfterAnimation()
    {
        Destroy(this.gameObject);
    }

}
