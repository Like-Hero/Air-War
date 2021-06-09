using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //游戏道具
    public static int Prop_EnemyBulletClearAmount;//清除界面中所有的敌人子弹
    public static int Prop_EnemyPlaneClearAmount;//清除界面中所有的敌人并且直接加分

    public KeyCode EnemyBulletClearKeyCode;
    public KeyCode EnemyPlaneClearKeyCode;

    public GameObject Player;
    public GameObject EnemyCreator;
    public GameObject AwardCreator;
    public GameObject ResultInterface;

    private bool EnemyBulletClear_IsPressed;
    private bool EnemyPlaneClear_IsPressed;

    public static bool playerIsDead;
    public static bool gameIsPlaying;
    //public static bool gameIsPause;

    public GameObject startText;
    public GameObject tipsText;

    private void Start()
    {
        Prop_EnemyBulletClearAmount = 0;
        Prop_EnemyPlaneClearAmount = 0;
        gameIsPlaying = false;
        playerIsDead = false;
        //GamePause();
    }
    private void Update()
    {
        //游戏未开始
        if (!gameIsPlaying)
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
            if (playerIsDead && gameIsPlaying)
            {
                GameFinish();
            }
        }
    }
    public void GameFinish()
    {
        print("游戏结束");
        gameIsPlaying = false;
        Invoke("ShowResult", 1.0f);
    }
    private void ShowResult()
    {
        ResultInterface.SetActive(true);
        PlayerData.MaxScore = Mathf.Max(PlayerData.MaxScore, PlayerData.Score);
        Destroy(Player);
    }
    private void Prop_Update()
    {
        if (Input.GetKeyDown(EnemyBulletClearKeyCode) && Prop_EnemyBulletClearAmount > 0)
        {
            Prop_EnemyBulletClearAmount--;
            ClearAllEnemyBullet();
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
        if (Player != null)
        {
            Player.SetActive(true);
        }
        EnemyCreator.SetActive(true);
        AwardCreator.SetActive(true);
        startText.SetActive(false);
        tipsText.SetActive(false);
    }
    //private void GamePause()
    //{
    //    gameIsPause = true;
    //}
    public static void ClearAllEnemyPlane()
    {
        GameObject[] enemiesPlane = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject item in enemiesPlane)
        {
            item.GetComponent<Enemy>().hp = 0;
        }
    }
    public static void ClearAllEnemyBullet()
    {
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (GameObject item in enemyBullets)
        {
            Destroy(item);
        }
    }
}
