using TMPro;
using UnityEngine;

public class AchievementUI : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private TMP_Text enemiesKilledText;
    [SerializeField] private TMP_Text survivalTimeText;
    [SerializeField] private TMP_Text totalDmgText;
    [SerializeField] private TMP_Text totalMoneyText;
    private int hour;
    private float min;
    private float sec;
    #endregion

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        enemiesKilledText = transform.Find("EnemiesKilledText").GetComponent<TMP_Text>();
        survivalTimeText = transform.Find("SurvivalTimeText").GetComponent<TMP_Text>();
        totalDmgText = transform.Find("TotalDamageText").GetComponent<TMP_Text>();
        totalMoneyText = transform.Find("TotalMoneyText").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        ShowEnemiesKilledText();
        ShowSurvivalTimeText();
        ShowTotalDamageText();
        ShowTotalMoneyText();
    }

    private void ShowTotalMoneyText()
    {
        totalMoneyText.text = "Total Money: " + Achievement.Instance.TotalMoney.ToString();
    }

    private void ShowEnemiesKilledText()
    {
        enemiesKilledText.text = "Enemies Killed: " + Achievement.Instance.EnemiesKilled.ToString();
    }

    private void ShowSurvivalTimeText()
    {
        min = Mathf.FloorToInt(Achievement.Instance.SurvivalTime / 60);
        sec = Mathf.FloorToInt(Achievement.Instance.SurvivalTime % 60);
        if (min == 60)
        {
            hour++;
            Achievement.Instance.SurvivalTime = 0;
        }

        survivalTimeText.text = string.Format("Survival Time:  {0:00}:{1:00}:{2:00}", hour, min, sec);
    }

    private void ShowTotalDamageText()
    {
        totalDmgText.text = "Total Damage: " + Achievement.Instance.TotalDmg.ToString();
    }
}
