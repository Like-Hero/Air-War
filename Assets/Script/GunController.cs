using System;
using UnityEngine;

public class GunController : MonoBehaviour  
{
    public GameObject bullet;

    private bool isCreat;

    public float rate;
    private void Update()
    {
        if (!GameManager.Ins.gameIsPlaying) return;
        if (CompareTag("PlayerGun"))
        {
            if (!GetComponentInParent<Transform>().GetComponentInParent<PlayerController>().isDead)
            {
                if (!isCreat)
                {
                    isCreat = true;
                    Invoke("Shoot", rate);
                }
            }
        }
        else if (CompareTag("EnemyGun"))
        {
            if (GameManager.Ins.gameIsPlaying)
            {
                if (!isCreat)
                {
                    isCreat = true;
                    Invoke("Shoot", rate);
                }
            }
        }
    }
    private void Shoot()
    {
        if (CompareTag("PlayerGun"))
        {
            Instantiate(bullet, transform.position, Quaternion.identity).tag = "PlayerBullet";
        }else if(CompareTag("EnemyGun")){
            Instantiate(bullet, transform.position, Quaternion.identity).tag = "EnemyBullet";
        }
        isCreat = false;
    }
}
