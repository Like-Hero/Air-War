using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //游戏道具
    public int Prop_EnemyBulletClearAmount;//清除界面中所有的敌人子弹
    public int Prop_EnemyPlaneClearAmount;//清除界面中所有的敌人并且直接加分

    public KeyCode EnemyBulletClearKeyCode;
    public KeyCode EnemyPlaneClearKeyCode;

    public GameObject EnemyCreator;
    public GameObject AwardCreator;
    public GameObject ResultInterface;

    public GameObject[] HPImgs;

    private bool EnemyBulletClear_IsPressed;
    private bool EnemyPlaneClear_IsPressed;

    public GameObject[] players;

    public bool gameIsPlaying;
    public bool gameIsFinish;
    //public bool gameIsPause;

    public GameObject startText;
    public GameObject tipsText;

    public float fireUpgradeContinueTime;
    public float fireUpgradeNowTime;
    private bool fireIsUpgrade;
    public int fireLevel;
    public GameObject FireImgBar;

    private static GameManager _ins;
    public static GameManager Ins { get { return _ins; } }
    private void Start()
    {
        if (!_ins)
        {
            _ins = this;
        }
        //GamePause();
    }
    private void Update()
    {
        
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
                    FireImgBar.SetActive(fireIsUpgrade);
                    fireUpgradeNowTime = 0;
                    fireLevel = 0;
                    ResumeFire();
                }
            }
            if (gameIsPlaying && JudgeAllPlayerIsDead())
            {
                GameFinish();
            }
        }
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

    public void GameFinish()
    {
        print("游戏结束");
        gameIsFinish = true;
        gameIsPlaying = false;
        Invoke("ShowResult", 1.0f);
    }
    private void ShowResult()
    {
        ResultInterface.SetActive(true);
        PlayerData.MaxScore = Mathf.Max(PlayerData.MaxScore, PlayerData.Score);
    }
    private void Prop_Update()
    {
        if (Input.GetKeyDown(EnemyBulletClearKeyCode) && Prop_EnemyBulletClearAmount > 0)
        {
            Prop_EnemyBulletClearAmount--;
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
    //private void GamePause()
    //{
    //    gameIsPause = true;
    //}
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
