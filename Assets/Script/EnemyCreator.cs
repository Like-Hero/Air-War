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

    public Enemy[] enemys;

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
        int index = Random.Range(0, enemys.Length);
        float bornX = Random.Range(leftBoundary.transform.position.x + 0.5f, rightBoundary.transform.position.x - 0.5f);
        Enemy enemy = Instantiate(enemys[index], new Vector3(bornX, transform.position.y, 0), Quaternion.identity);
        isCreat = false;
    }
}
