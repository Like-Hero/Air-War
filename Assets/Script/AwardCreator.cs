using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardCreator : MonoBehaviour
{
    public Award[] awards;

    private bool isCreat;

    public float minDelayTime;
    public float maxDelayTime;

    public Transform leftBoundary;
    public Transform rightBoundary;

    private void Update()
    {
        if (!GameManager.Ins.gameIsPlaying) return;
        if (!isCreat)
        {
            isCreat = true;
            float delayTime = Random.Range(minDelayTime, maxDelayTime);
            Invoke("CreatAward", delayTime);
        }
    }
    private void CreatAward()
    {
        int index = Random.Range(0, awards.Length);
        float bornX = Random.Range(leftBoundary.transform.position.x + 0.5f, rightBoundary.transform.position.x - 0.5f);
        Award award = Instantiate(awards[index], new Vector3(bornX, transform.position.y, 0), Quaternion.identity);
        isCreat = false;
    }
}
