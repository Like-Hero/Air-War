using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Award : MonoBehaviour
{
    public int speed;
    private void Update()
    {
        if (transform.position.y < GameObject.Find("DestroyDownPoint").transform.position.y)
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        transform.Translate(Vector3.down * speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player_1") || other.CompareTag("Player_2"))
        {
            if (CompareTag("EnemyBulletClear"))
            {
                GameManager.Ins.Prop_EnemyBulletClearAmount++;
            }else if (CompareTag("EnemyPlaneClear"))
            {
                GameManager.Ins.Prop_EnemyPlaneClearAmount++;
            }
            Destroy(gameObject);
        }
    }
}
