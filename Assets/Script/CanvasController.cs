using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Text EnemyBulletClear_Text;
    public Text EnemyPlaneClear_Text;
    private void Update()
    {
        UpdateCollectionValue();
    }
    private void UpdateCollectionValue()
    {
        EnemyBulletClear_Text.text = GameManager.Ins.Prop_FireUpgradeAmount.ToString();
        EnemyPlaneClear_Text.text = GameManager.Ins.Prop_EnemyPlaneClearAmount.ToString();
    }
    public void Restart()
    {
        GameManager.Ins.gameIsPlaying = true;
        PlayerData.Score = PlayerData.PrevScore;//恢复上一关的分数
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
