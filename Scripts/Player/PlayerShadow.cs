using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer currentSprite;
    private SpriteRenderer playerSprite;
    private Color color;

    [Header("时间")]
    public float activeTime;//显示持续时间
    public float startTime;//开始显示时间

    [Header("不透明度控制")]
    private float alpha;
    public float alphaSet;//不透明度初始值
    public float alphaMultipliter;//alph不断乘以一个小于1的数值，实现逐渐透明
    private void OnEnable() 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentSprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();//获得player身上的图片组件

        //设置alpha
        alpha = alphaSet;
        //传输图像和位置
        currentSprite.sprite = playerSprite.sprite;
        transform.position = player.position;
        transform.localScale = player.localScale;
        transform.rotation = player.rotation;

        //启动时间点等于当前时间
        activeTime = Time.time;

        
    }
    private void FixedUpdate() 
    {
        alpha *= alphaMultipliter;
        color = new Color(1f , 1 ,1f,alpha);

        currentSprite.color = color;

        if(Time.time >= startTime + activeTime)
        {
            //返回对象池
            Pool.instance.ReturnPool(this.gameObject);
        }
    }
}
