using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FireImgController : MonoBehaviour
{
    private Image img;
    private Animator anim;
    public GameObject parent;
    public GameObject LevelText;
    private float fireTimePercent;
    public float minFireTime;
    private void Start()
    {
        anim = GetComponent<Animator>();
        img = GetComponent<Image>();
    }
    private void Update()
    {
        if (GameManager.Ins.gameIsPause) return;
        fireTimePercent = 1.0f - GameManager.Ins.fireUpgradeNowTime / GameManager.Ins.fireUpgradeContinueTime;
        if (LevelText.GetComponent<TMP_Text>() != null)
        {
            LevelText.GetComponent<TMP_Text>().text = "Level:  " + (GameManager.Ins.fireLevel + 1).ToString();
        }
        if (GameManager.Ins.fireUpgradeNowTime != 0)
        {
            anim.SetFloat("fireTime", fireTimePercent);
            img.fillAmount = fireTimePercent;
        }
        else
        {
            img.fillAmount = 0;
        }
    }
}
