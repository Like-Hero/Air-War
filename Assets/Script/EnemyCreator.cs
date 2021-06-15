using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    public GameObject enemy;

    public float minDelayTime;
    public float maxDelayTime;

    public bool isCreat;

    public Transform leftBoundary;
    public Transform rightBoundary;

    public GameObject[] enemys;

    private void Update()
    {
        if (GameManager.Ins.gameIsPause) return;
        if (!GameManager.Ins.gameIsPlaying) return;
        if (!isCreat)
        {
            isCreat = true;
            float delayTime = Random.Range(minDelayTime, maxDelayTime);
            Invoke("CreatEnemy", delayTime);
        }
    }
    private void CreatEnemy()
    {
        GameObject enemy = null;
        int index = Random.Range(0, enemys.Length);
        float bornX = Random.Range(leftBoundary.transform.position.x + 0.5f, rightBoundary.transform.position.x - 0.5f);
        Vector3 pos = new Vector3(bornX, transform.position.y, 0);
        if (GameManager.Ins.enemiesPool[index].Count > 0)
        {
            enemy = GameManager.Ins.enemiesPool[index][0];
            GameManager.Ins.enemiesPool[index].RemoveAt(0);
            enemy.transform.position = pos;
            enemy.SetActive(true);
        }
        else
        {
            enemy = Instantiate(enemys[index], pos, Quaternion.identity);
        }
        isCreat = false;
    }
}
