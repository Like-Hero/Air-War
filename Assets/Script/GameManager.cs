using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //游戏道具
    public int Prop_FireUpgradeAmount;//火力升级
    public int Prop_EnemyPlaneClearAmount;//清除界面中所有的敌人并且直接加分

    public KeyCode EnemyBulletClearKeyCode;
    public KeyCode EnemyPlaneClearKeyCode;

    public GameObject EnemyCreator;
    public GameObject AwardCreator;
    public GameObject ResultInterface;

    public GameObject[] HPImgs;

    public GameObject[] players;

    public bool gameIsPlaying;
    public bool gameIsFinish;
    public bool gameIsPause;

    public GameObject startText;
    public GameObject tipsText;

    public float fireUpgradeContinueTime;
    public float fireUpgradeNowTime;
    private bool fireIsUpgrade;
    public int fireLevel;
    public GameObject FireImgBar;

    //子弹池
    public List<GameObject> playerBulletPool;
    public List<GameObject> trackBulletPool;
    public List<GameObject> enemy0BulletPool;
    public List<GameObject> enemy1BulletPool;
    public List<GameObject> enemy2BulletPool;

    private static GameManager _ins;
    public static GameManager Ins { get { return _ins; } }
    private void Start()
    {
        if (!_ins)
        {
            _ins = this;
        }
        playerBulletPool = new List<GameObject>();
        enemy0BulletPool = new List<GameObject>();
        enemy1BulletPool = new List<GameObject>();
        enemy2BulletPool = new List<GameObject>();
        TxTIO.Init();
        TxTIO.ReadMaxScore();
    }
    private void Update()
    {
        if (gameIsPause)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameResume();
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GamePause();
        }
        //游戏未开始
        if (!gameIsPlaying && !gameIsFinish)
        {
            if (Input.anyKeyDown)
            {
                GameStart();
            }
        }
        else//游戏开始
        {
            //道具使用
            Prop_Update();
            //升级火力
            if (fireIsUpgrade)
            {
                fireUpgradeNowTime += Time.deltaTime;
                FireImgBar.SetActive(fireIsUpgrade);
                if (fireUpgradeNowTime >= fireUpgradeContinueTime)
                {
                    //时间到了，恢复火力
                    fireIsUpgrade = false;
                    fireUpgradeNowTime = 0;
                    fireLevel = 0;
                    FireImgBar.SetActive(fireIsUpgrade);
                    ResumeFire();
                }
            }
            if (gameIsPlaying && JudgeAllPlayerIsDead())
            {
                GameFinish();
            }
        }
        PlayerData.MaxScore = Mathf.Max(PlayerData.MaxScore, PlayerData.Score);
    }
    public GameObject MakeBullet(GameObject bullet, Vector3 pos, string tag)
    {
        if (tag.Equals("PlayerGun"))
        {
            if(playerBulletPool.Count > 0)
            {
                //从子弹池取出一个子弹
                bullet = playerBulletPool[0];
                playerBulletPool.RemoveAt(0);
                bullet.transform.position = pos;
                bullet.SetActive(true);
            }
            else
            {
                //新建一个子弹
                bullet = Instantiate(bullet, pos, Quaternion.identity);
            }
        }else if (tag.Equals("TrackGun"))
        {
            if (trackBulletPool.Count > 0)
            {
                //从子弹池取出一个子弹
                bullet = trackBulletPool[0];
                trackBulletPool.RemoveAt(0);
                bullet.transform.position = pos;
                bullet.SetActive(true);
            }
            else
            {
                //新建一个子弹
                bullet = Instantiate(bullet, pos, Quaternion.identity);
            }
        }
        else if (tag.Equals("Enemy0Gun"))
        {
            if(enemy0BulletPool.Count > 0)
            {
                bullet = enemy0BulletPool[0];
                enemy0BulletPool.RemoveAt(0);
                bullet.transform.position = pos;
                bullet.SetActive(true);
            }
            else
            {
                bullet = Instantiate(bullet, pos, Quaternion.identity);
            }
        }
        else if (tag.Equals("Enemy1Gun"))
        {
            if(enemy1BulletPool.Count > 0)
            {
                bullet = enemy1BulletPool[0];
                enemy1BulletPool.RemoveAt(0);
                bullet.transform.position = pos;
                bullet.SetActive(true);
            }
            else
            {
                bullet = Instantiate(bullet, pos, Quaternion.identity);
            }
        }
        else if (tag.Equals("Enemy2Gun"))
        {
            if (enemy2BulletPool.Count > 0)
            {
                bullet = enemy2BulletPool[0];
                enemy2BulletPool.RemoveAt(0);
                bullet.transform.position = pos;
                bullet.SetActive(true);
            }
            else
            {
                bullet = Instantiate(bullet, pos, Quaternion.identity);
            }
        }
        return bullet;
    }
    private bool JudgeAllPlayerIsDead()
    {
        foreach (GameObject player in players)
        {
            if (!player.GetComponent<PlayerController>().isDead)
            {
                return false;
            }
        }
        return true;
    }
    private void ShowResult()
    {
        ResultInterface.SetActive(true);
    }
    private void Prop_Update()
    {
        if (Input.GetKeyDown(EnemyBulletClearKeyCode) && Prop_FireUpgradeAmount > 0)
        {
            Prop_FireUpgradeAmount--;
            fireIsUpgrade = true;
            fireUpgradeNowTime = 0;
            //ClearAllEnemyBullet();
            UpgradeFire();
        }
        if (Input.GetKeyDown(EnemyPlaneClearKeyCode) && Prop_EnemyPlaneClearAmount > 0)
        {
            Prop_EnemyPlaneClearAmount--;
            ClearAllEnemyPlane();
        }
    }
    private void GameStart()
    {
        gameIsPlaying = true;
        EnemyCreator.SetActive(true);
        AwardCreator.SetActive(true);
        startText.SetActive(false);
        tipsText.SetActive(false);
        foreach (GameObject HPImg in HPImgs)
        {
            HPImg.SetActive(true);
        }
    }
    private void GamePause()
    {
        ShowResult();
        gameIsPause = true;
    }
    private void GameResume()
    {
        gameIsPause = false;
        ResultInterface.SetActive(false);
    }
    public void GameFinish()
    {
        print("游戏结束");
        gameIsFinish = true;
        gameIsPlaying = false;
        TxTIO.WriteIntoTxt(PlayerData.Score.ToString());//写入当前分数
        PlayerData.MaxScore = Mathf.Max(PlayerData.MaxScore, TxTIO.ReadMaxScore());//更新当前最大分数
        Invoke("ShowResult", 1.0f);
    }
    public void ClearAllEnemyPlane()
    {
        GameObject[] enemiesPlane = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject item in enemiesPlane)
        {
            item.GetComponent<Enemy>().hp = 0;
        }
    }
    public void ClearAllEnemyBullet()
    {
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (GameObject item in enemyBullets)
        {
            Destroy(item);
        }
    }
    private void UpgradeFire()
    {
        foreach (GameObject player in players)
        {
            fireLevel = player.transform.Find("Guns").GetComponent<GunsController>().UpgradeFire();
        }
    }
    private void ResumeFire()
    {
        foreach (GameObject player in players)
        {
            player.transform.Find("Guns").GetComponent<GunsController>().ResumeFire();
        }
    }
}
