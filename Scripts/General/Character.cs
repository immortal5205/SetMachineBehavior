using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基本属性")]
    public float maxHP;
    public float currentHP;
    [Header("角色受伤无敌")]
    public float invincibleTime;
    private float invincibleCount;//计时器,代码内运算

    public bool isInvincible;
    [Header("事件")]
    public UnityEvent<Transform> OnTakeDamage;//受伤击退，<传入坐标>
    public UnityEvent OnDie;
    private void Start() 
    {
        currentHP = maxHP;
        
        
    }

    private void Update() 
    {
        if(isInvincible)
        {
            invincibleCount -= Time.deltaTime;
            if(invincibleCount <= 0)
            {
                isInvincible = false;
            }
        }
        
    }

    public void TackDamage(Attack attack)
    {
        if(isInvincible)
            return;

        if(currentHP - attack.damage >0)
        { 
            currentHP -= attack.damage;
            TriggerInvincible();
            //执行受伤
            OnTakeDamage?.Invoke(attack.transform);//Invoke启动  传入攻击者的坐标

        }
        else
        {
            currentHP = 0;
            //触发死亡
            OnDie?.Invoke();
        }


    }

    private void TriggerInvincible()//触发无敌的函数
    {
        if(!isInvincible)
        {
            isInvincible = true;
            invincibleCount = invincibleTime;
        }

    }
}
