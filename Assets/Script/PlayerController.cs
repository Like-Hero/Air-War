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

    public bool isDead;
    private void Start()
    {
        nowHP = maxHP;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!GameManager.Ins.gameIsPlaying)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        JudgeDead();
    }
    private void FixedUpdate()
    {
        if (!GameManager.Ins.gameIsPlaying) return;
        if (!isDead)
        {
            Move();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    private void Move()
    {
        if (CompareTag("Player_1"))
        {
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
        }else if (CompareTag("Player_2"))
        {
            //键盘控制
            float xVelocity = Input.GetAxisRaw("Horizontal");
            float yVelocity = Input.GetAxisRaw("Vertical");
            transform.position = OnMap(transform.position);
            //设置速度
            rb.velocity = new Vector2(xVelocity * speed, yVelocity * speed);
        }
    }

    //保证不出界
    private Vector3 OnMap(Vector3 pos)
    {
        Transform left = GameObject.Find("Left").transform;
        Transform right = GameObject.Find("Right").transform;
        Transform up = GameObject.Find("Up").transform;
        Transform down = GameObject.Find("Down").transform;
        if (pos.x <= left.position.x)
        {
            pos.x = left.position.x;
        }
        if (pos.x >= right.position.x)
        {
            pos.x = right.position.x;
        }
        if (pos.y <= down.position.y)
        {
            pos.y = down.position.y;
        }
        if (pos.y >= up.position.y)
        {
            pos.y = up.position.y;
        }
        return pos;
    }

    private void JudgeDead()
    {
        if (GameManager.Ins.gameIsPlaying && nowHP <= 0 && !isDead)
        {
            anim.SetTrigger("Dead");
            isDead = true;
        }
    }
    private void Dead()
    {
        //Destroy(gameObject);
        GetComponent<Renderer>().enabled = false;//使游戏对象消失
        gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = false;//防止子弹继续有碰撞
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
