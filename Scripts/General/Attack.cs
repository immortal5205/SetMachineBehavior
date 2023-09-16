using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("伤害")]
    public int damage;
    public float attackRange;//检测距离
    public float attactRate;

    //攻击伤害碰撞
    public Vector3 attackOffset;
    public LayerMask attackLayer;
    private Vector3 pos;


    private void OnTriggerStay2D(Collider2D other) 
    {
        other.GetComponent<Character>()?.TackDamage(this);//加上？判断other上是否挂载了Character       
    }

    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,attackRange);
    }

    public void ATK()
    {
        pos = transform.position;

        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D coll = Physics2D.OverlapCircle(pos , attackRange , attackLayer);
        if(coll != null)
        {
            coll.GetComponent<Character>().TackDamage(this);
        }
    }

}
