using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
   
     [Header("速度")]
     public float speed;
     public float walkSpeed;
     public float runSpeed;
     [Header("跳跃")]
     public float jumpForce;
     public bool isJump;
     public bool doubleJump;

    [Header("组件")]
     private Rigidbody2D rb;
     private Collider2D coll;
     public Vector2 inputDirection;
     public PlayerInputControl inputContronl;
     private PhysicsCheck physicsCheck;
     private PlayerAnimation playerAnimation;
     public Slider playerHPBar;
     private Character character;
     public Image coolTime;
     public AudioSource audioSource;
     
    

     [Header("状态")]
     public bool isHurt;
     public float hurtForce;//受伤反弹的力
     public bool isDead;
     public bool isAttack;
     public bool isDash;
     public bool isWalk;
     [Header("Dash参数")]
     public float dashTime;//冲锋持续时间
     private float dashTimeleft;//冲锋剩余时间
     private float lastDash = -10f;//上一次冲锋时间
     public float dashSpeed;
     public float dashCoolTime;
     [Header("walk时间间隔")]
     public float walkTime;
     private float time;
        
   private void Awake() 
   {
          rb = GetComponent<Rigidbody2D>();
          coll = GetComponent<Collider2D>();
          physicsCheck = GetComponent<PhysicsCheck>();
          playerAnimation = GetComponent<PlayerAnimation>();
          inputContronl = new PlayerInputControl();
          character = GetComponent<Character>();
          audioSource = GetComponent<AudioSource>();
         
        

       
       //跳跃是事件  攻击
          inputContronl.GamePlay.Jump.started += Jump;
          inputContronl.GamePlay.Attack.started += PlayerAttack;
   
    
   }



    private void OnEnable() 
   {
          inputContronl.Enable();

   }
   private void OnDisable() 
   {
          inputContronl.Disable();
   }
   private void Update() 
   {
          inputDirection = inputContronl.GamePlay.Move.ReadValue<Vector2>();
          playerHPBar.value = character.currentHP;
          coolTime.fillAmount -= 1f/dashCoolTime * Time.deltaTime;
        
       if(Input.GetKeyDown(KeyCode.K))
       {
              if(Time.time >= lastDash + dashCoolTime)
              {
                     //可以Dash
                     ReadyDash();              
                     
              }
       }
     
            time -= Time.deltaTime; 
   }
   private void FixedUpdate() 
   {
       if(physicsCheck.isGround && !isHurt && !isAttack && !isDash && time <=0)
       {  
              Move();
              //移动时的声音播放
              if((inputDirection.x>0 || inputDirection.x<0)&& !audioSource.isPlaying)
              {
                     audioSource.Play();
              }
              else
              {
                     audioSource.Stop();
              }
              time = walkTime;
             
       
       }


       Dash();

       if(isDash)
       {
              gameObject.layer = 2;
             
       }else
       {
              gameObject.layer = 6;
       }
      
    
     
     
   }

   //     //测试
   // private void OnTriggerStay2D(Collider2D other) 
   // {
   //     Debug.Log(other.name);
       
   // }

   public void Move()//玩家移动
   {
       
        rb.velocity = new Vector2(inputDirection.x*speed*Time.deltaTime,rb.velocity.y);

          //人物翻转
          int faceDir = (int)transform.localScale.x;//所以创建临时变量记录临时方向
          if (inputDirection.x >0){
               faceDir = 20 ;
          }
          if (inputDirection.x <0){
               faceDir = -20 ;
          }
        

          transform.localScale = new Vector3(faceDir , 20 ,20);
         
      
         
   }

   private void Jump(InputAction.CallbackContext context)//玩家跳跃
    {
       if(physicsCheck.isGround)
       {
              rb.AddForce(transform.up*jumpForce , ForceMode2D.Impulse);//ForceMode2D.Impulse 为施加力的方式，它可选择其它的
              isJump = true;
              doubleJump = true;
       }else if(doubleJump)
       {
              rb.AddForce(transform.up*jumpForce*2 , ForceMode2D.Impulse);
              doubleJump = false;
              isJump = true;
       }
        if(isJump)
       GetComponent<AudioDefination>()?.PlayAudioClip();
    }
    private void Dash()//冲刺
    {
 
       if(isDash)
       {
            
              
              if(dashTimeleft >0)
              {
                     isHurt = false;
                     
                     if(rb.velocity.y>0 && !physicsCheck.isGround)
                     {
                             rb.velocity = new Vector2(dashSpeed * transform.localScale.x ,jumpForce);//跳跃的时候向上冲刺，而不是下坠
                     }
                     rb.velocity = new Vector2(dashSpeed * transform.localScale.x , rb.velocity.y);

                     dashTimeleft -= Time.deltaTime;
                     
                     Pool.instance.GetFromPool();
                    
              }
               if(dashTimeleft <= 0)
              {
                     isDash = false;
                     
                     if(!physicsCheck.isGround)
                     {
                             rb.velocity = new Vector2(dashSpeed * transform.localScale.x , jumpForce);//跳跃结束时，但不在地面上会再次向上冲刺一段距离
                     }
                     
                     
              }
             
       }
      
       
    }

    public void GetHurt(Transform attack)//受伤击飞  得到攻击者的Trans
    {  
       isHurt = true;
       rb.velocity = Vector2.zero;
       Vector2 dir = new Vector2((transform.position.x - attack.transform.position.x) , 0).normalized;//计算受伤的方向  normalized 数值归一化，防止坐标差值乘反方向力后结果过大

       rb.AddForce(dir*hurtForce,ForceMode2D.Impulse);

    }

     private void PlayerAttack(InputAction.CallbackContext context)//玩家攻击
    {
        playerAnimation.PlayerAttack();
        isAttack = true;
        
    }


    public void PlayerDead()//玩家死亡
    {
       isDead = true;
      
       inputContronl.GamePlay.Disable();//只关闭角色控制
       isAttack = true;
    }

    public void ReadyDash()
    {
       isDash = true;
       dashTimeleft = dashTime;

       lastDash = Time.time;
       coolTime.fillAmount = 1;
      
    }

 

}