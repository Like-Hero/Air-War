using System;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour  
{
    public GameObject bullet;

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
        GameObject newBullet = null;
        if (CompareTag("PlayerGun"))
        {
            if (GameManager.Ins.playerBulletPool.Count > 0)
            {
                //从子弹池取出一个子弹
                newBullet = GameManager.Ins.playerBulletPool[0];
                GameManager.Ins.playerBulletPool.RemoveAt(0);
                newBullet.transform.position = transform.position;
                newBullet.SetActive(true);
            }
            else
            {
                //新建一个子弹
                newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            }
        }
        else if (CompareTag("TrackGun"))
        {
            if (GameManager.Ins.trackBulletPool.Count > 0)
            {
                //从子弹池取出一个子弹
                bullet = GameManager.Ins.trackBulletPool[0];
                GameManager.Ins.trackBulletPool.RemoveAt(0);
                bullet.transform.position = transform.position;
                bullet.SetActive(true);
            }
            else
            {
                //新建一个子弹
                newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            }
        }
        else if (CompareTag("Enemy0Gun"))
        {
            if (GameManager.Ins.enemiesBulletPool[0].Count > 0)
            {
                newBullet = GameManager.Ins.enemiesBulletPool[0][0];
                GameManager.Ins.enemiesBulletPool[0].RemoveAt(0);
                newBullet.transform.position = transform.position;
                newBullet.SetActive(true);
            }
            else
            {
                newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            }
        }
        else if (CompareTag("Enemy1Gun"))
        {
            if (GameManager.Ins.enemiesBulletPool[1].Count > 0)
            {
                newBullet = GameManager.Ins.enemiesBulletPool[1][0];
                GameManager.Ins.enemiesBulletPool[1].RemoveAt(0);
                newBullet.transform.position = transform.position;
                newBullet.SetActive(true);
            }
            else
            {
                newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            }
        }
        else if (CompareTag("Enemy2Gun"))
        {
            if (GameManager.Ins.enemiesBulletPool[2].Count > 0)
            {
                newBullet = GameManager.Ins.enemiesBulletPool[2][0];
                GameManager.Ins.enemiesBulletPool[2].RemoveAt(0);
                newBullet.transform.position = transform.position;
                newBullet.SetActive(true);
            }
            else
            {
                newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            }
        }
        isCreat = false;
    }
}
