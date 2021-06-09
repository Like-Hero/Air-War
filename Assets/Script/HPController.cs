using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    private Image hpImage;
    public GameObject player;
    private void Start()
    {
        hpImage = GetComponent<Image>();
    }
    private void Update()
    {
        if (GameManager.gameIsPlaying && player != null)
        {
            hpImage.fillAmount = (float)player.GetComponent<PlayerController>().nowHP / player.GetComponent<PlayerController>().maxHP;
        }
        else
        {
            hpImage.fillAmount = 0;
        }
    }
}
