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
        if (other.CompareTag("Player"))
        {
            if (CompareTag("EnemyBulletClear"))
            {
                GameManager.Prop_EnemyBulletClearAmount++;
            }else if (CompareTag("EnemyPlaneClear"))
            {
                GameManager.Prop_EnemyPlaneClearAmount++;
            }
            Destroy(gameObject);
        }
    }
}
