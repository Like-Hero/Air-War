using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FireImgController : MonoBehaviour
{
    private Image img;
    public GameObject parent;
    public GameObject LevelText;
    private void Start()
    {
        img = GetComponent<Image>();
    }
    private void Update()
    {
        if(LevelText.GetComponent<TMP_Text>() != null)
        {
            LevelText.GetComponent<TMP_Text>().text = "Level:  " + (GameManager.Ins.fireLevel + 1).ToString();
        }
        if (GameManager.Ins.fireUpgradeNowTime != 0)
        {
            img.fillAmount = 1.0f - GameManager.Ins.fireUpgradeNowTime / GameManager.Ins.fireUpgradeContinueTime;
        }
        else
        {
            img.fillAmount = 0;
        }
    }
}
