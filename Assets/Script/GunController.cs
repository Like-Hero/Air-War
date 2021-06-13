using System;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour  
{
    public GameObject bulletPrefabs;

    private bool isCreat;

    public float rate;

    private void Update()
    {
        if (!GameManager.Ins.gameIsPlaying) return;
        if (CompareTag("PlayerGun") || CompareTag("TrackGun"))
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
        else if (CompareTag("Enemy0Gun") || CompareTag("Enemy1Gun") || CompareTag("Enemy2Gun"))
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
        GameObject newBullet = GameManager.Ins.MakeBullet(bulletPrefabs, transform.position, tag);
        if (CompareTag("PlayerGun"))
        {
            newBullet.tag = "PlayerBullet";
        }
        else if (CompareTag("TrackGun"))
        {
            newBullet.tag = "TrackBullet";
        }
        else if (CompareTag("Enemy0Gun"))
        {
            newBullet.tag = "Enemy0Bullet";
        }
        else if (CompareTag("Enemy1Gun"))
        {
            newBullet.tag = "Enemy1Bullet";
        }
        else if (CompareTag("Enemy2Gun"))
        {
            newBullet.tag = "Enemy2Bullet";
        }

        //if (CompareTag("PlayerGun"))
        //{
        //    Instantiate(bullet, transform.position, Quaternion.identity).tag = "PlayerBullet";
        //}else if(CompareTag("EnemyGun")){
        //    Instantiate(bullet, transform.position, Quaternion.identity).tag = "EnemyBullet";
        //}
        isCreat = false;
    }
}
