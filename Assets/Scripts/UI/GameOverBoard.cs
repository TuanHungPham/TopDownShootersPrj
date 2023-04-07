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
    [SerializeField] private GameObject gameOverSceneColor;
    private float min, sec, hour;
    #endregion

    private void OnEnable()
    {
        gameOverSceneColor.SetActive(true);
    }

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
        gameOverSceneColor = transform.root.GetChild(0).Find("GameOverSceneColor").gameObject;
    }

    private void Update()
    {
        ShowAchievement();
    }

    private void ShowAchievement()
    {
        enemiesKilledText.text = Achievement.Instance.enemiesKilled.ToString();
        totalDamageText.text = Achievement.Instance.totalDmg.ToString();
        totalMoneyText.text = Achievement.Instance.totalMoney.ToString();
        ShowSurvivalTimeText();
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

    private void OnDisable()
    {
        gameOverSceneColor.SetActive(false);
    }
}
