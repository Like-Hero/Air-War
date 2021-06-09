using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public float speed;
    public int maxHP;
    public int nowHP;

    private Vector3 screenPosition;//将物体从世界坐标转换为屏幕坐标
    private Vector3 mousePositionOnScreen;//获取到点击屏幕的屏幕坐标
    private Vector3 mousePositionInWorld;//将点击屏幕的屏幕坐标转换为世界坐标

    private bool isDead;
    private void Start()
    {
        nowHP = maxHP;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        JudgeDead();
    }
    private void FixedUpdate()
    {
        if (GameManager.gameIsPlaying)
        {
            Move();
        }
    }
    private void Move()
    {
        //键盘控制
        //float xVelocity = Input.GetAxisRaw("Horizontal");
        //float yVelocity = Input.GetAxisRaw("Vertical");
        ////设置速度
        //rb.velocity = new Vector2(xVelocity * speed, yVelocity * speed);


        //鼠标控制
        //获取鼠标在场景中的屏幕坐标
        mousePositionOnScreen = Input.mousePosition;
        if (mousePositionOnScreen.x <= 0 || mousePositionOnScreen.x >= Screen.width || mousePositionOnScreen.y <= 0 || mousePositionOnScreen.y >= Screen.height)
        {
            return;
        }
        //将 position 从世界空间变换为屏幕空间
        screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        //让场景中的Z=鼠标坐标的Z
        mousePositionOnScreen.z = screenPosition.z;
        //将相机中的坐标转化为世界坐标
        mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
        //物体跟随鼠标移动
        transform.position = mousePositionInWorld;
        ////物体跟随鼠标X轴移动
        //transform.position = new Vector3(mousePositionInWorld.x, transform.position.y, transform.position.z);
    }
    private void JudgeDead()
    {
        if (GameManager.gameIsPlaying && nowHP <= 0 && !isDead)
        {
            anim.SetTrigger("Dead");
            GetComponent<Renderer>().enabled = false;//使游戏对象消失
            GetComponent<Collider2D>().enabled = false;//防止子弹继续有碰撞
            isDead = true;
        }
    }
    private void NoticeGameManagerDead()
    {
        GameManager.playerIsDead = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            nowHP -= other.GetComponent<BulletController>().attack;
            Destroy(other.gameObject);
        }
    }
    
}
