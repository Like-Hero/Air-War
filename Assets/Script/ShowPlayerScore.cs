using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerScore : MonoBehaviour
{
    public Text ScoreText;
    private void Update()
    {
        if (GameManager.gameIsPlaying)
        {
            ScoreText.text = "Score：" + PlayerData.Score.ToString();
        }
    }
}
