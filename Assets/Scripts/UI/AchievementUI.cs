using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementUI : MonoBehaviour
{
    #region public var
    public TMP_Text enemiesKilledText;
    public TMP_Text survivalTimeText;
    #endregion

    #region private var
    [SerializeField] private Achievement achievement;
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
        achievement = GameObject.Find("------ OTHER ------").transform.Find("Achievement").GetComponent<Achievement>();
        enemiesKilledText = transform.Find("EnemiesKilledText").GetComponent<TMP_Text>();
        survivalTimeText = transform.Find("SurvivalTimeText").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        ShowText();
    }

    private void ShowText()
    {
        enemiesKilledText.text = "Enemies Killed: " + achievement.enemiesKilled.ToString();
    }
}
