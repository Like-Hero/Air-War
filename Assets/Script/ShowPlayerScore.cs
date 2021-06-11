using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerScore : MonoBehaviour
{
    public Text ScoreText;
    private void Update()
    {
        if (GameManager.Ins.gameIsPlaying)
        {
            ScoreText.text = "Score：" + PlayerData.Score.ToString();
        }
    }
}
