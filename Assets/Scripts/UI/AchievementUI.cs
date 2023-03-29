using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementUI : MonoBehaviour
{
    #region public var
    public TMP_Text enemiesKilledText;
    public TMP_Text survivalTimeText;
    public TMP_Text totalDmgText;
    #endregion

    #region private var
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
    }

    private void Update()
    {
        ShowEnemiesKilledText();
        ShowSurvivalTimeText();
        ShowTotalDamageText();
    }

    private void ShowEnemiesKilledText()
    {
        enemiesKilledText.text = "Enemies Killed: " + Achievement.Instance.enemiesKilled.ToString();
    }

    private void ShowSurvivalTimeText()
    {
        min = Mathf.FloorToInt(Achievement.Instance.survivalTime / 60);
        sec = Mathf.FloorToInt(Achievement.Instance.survivalTime % 60);
        if (min == 60)
        {
            hour++;
            Achievement.Instance.survivalTime = 0;
        }

        survivalTimeText.text = string.Format("Survival Time:  {0:00}:{1:00}:{2:00}", hour, min, sec);
    }

    private void ShowTotalDamageText()
    {
        totalDmgText.text = "Total Damage: " + Achievement.Instance.totalDmg.ToString();
    }
}
