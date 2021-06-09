using UnityEngine;
using UnityEngine.UI;

public class ResultInterface : MonoBehaviour
{
    private Animator anim;
    public Text NowScoreText;
    public Text MaxScoreText;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void ShowFinalScoreText()
    {
        ShowNowScoreText();
        Invoke("ShowMaxScoreText", 0.5f);
    }
    private void ShowNowScoreText()
    {
        NowScoreText.gameObject.SetActive(true);
        NowScoreText.text = PlayerData.Score.ToString();
    }
    private void ShowMaxScoreText()
    {
        MaxScoreText.gameObject.SetActive(true);
        MaxScoreText.text = PlayerData.MaxScore.ToString();
    }
}
