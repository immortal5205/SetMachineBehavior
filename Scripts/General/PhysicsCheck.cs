using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public Vector2 bottomOffset;//绘制脚底检测范围  bottomOffset脚底位移差值
    public Vector2 LOffset;
    public Vector2 ROffset;
    [Header("地面检测,指定Layer,检测范围")]
    public float checkRadius;//检测固定范围
    public LayerMask Layer;//LayerMask筛选指定layer
    [Header("状态")]
    public bool isGround;
    public bool LtouchWall;
     public bool RtouchWall;

    private void Update() 
    {
         Check();
        
    }
    public void Check()
    {
        //检测地面
      isGround =  Physics2D.OverlapCircle((Vector2)transform.position+bottomOffset , checkRadius , Layer);
      //墙体检测
      LtouchWall = Physics2D.OverlapCircle((Vector2)transform.position+LOffset , checkRadius , Layer);
      RtouchWall = Physics2D.OverlapCircle((Vector2)transform.position+ROffset , checkRadius , Layer);
        
    }
    private void OnDrawGizmosSelected() 
    {
        Gizmos.DrawWireSphere((Vector2)transform.position+bottomOffset , checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position+LOffset , checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position+ROffset , checkRadius);
    }
}
