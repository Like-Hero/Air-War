﻿using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public int attack;
    private void Update()
    {
        if(transform.position.y > GameObject.Find("DestroyUpPoint").transform.position.y)
        {
            Destroy(gameObject);
        }else if(transform.position.y < GameObject.Find("DestroyDownPoint").transform.position.y)
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        if (CompareTag("PlayerBullet"))
        {
            transform.Translate(Vector3.up * speed * Time.fixedDeltaTime);
        }
        else if (CompareTag("EnemyBullet"))
        {
            transform.Translate(Vector3.down * speed * Time.fixedDeltaTime);
        }
    }
}