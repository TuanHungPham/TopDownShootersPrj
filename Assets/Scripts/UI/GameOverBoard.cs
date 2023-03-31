using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverBoard : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private TMP_Text enemiesKilledText;
    [SerializeField] private TMP_Text totalDamageText;
    [SerializeField] private TMP_Text timeSurvivedText;
    [SerializeField] private TMP_Text totalMoneyText;
    private float min, sec, hour;
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
        enemiesKilledText = transform.Find("EnemiesKilledText").Find("Number").GetComponent<TMP_Text>();
        totalDamageText = transform.Find("TotalDamageText").Find("Number").GetComponent<TMP_Text>();
        timeSurvivedText = transform.Find("TimeSurvivedText").Find("Number").GetComponent<TMP_Text>();
        totalMoneyText = transform.Find("TotalMoneyText").Find("Number").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        ShowAchievement();
        ShowSurvivalTimeText();
    }

    private void ShowAchievement()
    {
        enemiesKilledText.text = Achievement.Instance.enemiesKilled.ToString();
        totalDamageText.text = Achievement.Instance.totalDmg.ToString();
        totalMoneyText.text = Achievement.Instance.totalMoney.ToString();
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

        timeSurvivedText.text = string.Format("{0:00}:{1:00}:{2:00}", hour, min, sec);
    }
}
