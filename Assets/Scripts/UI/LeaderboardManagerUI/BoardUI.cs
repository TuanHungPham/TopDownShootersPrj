using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoardUI : MonoBehaviour
{
    #region public var
    public TMP_Text Number { get => number; set => number = value; }
    public TMP_Text UserName { get => userName; set => userName = value; }
    public TMP_Text Score { get => score; set => score = value; }
    #endregion

    #region private var
    [SerializeField] private TMP_Text number;
    [SerializeField] private TMP_Text userName;
    [SerializeField] private TMP_Text score;

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
        Number = transform.Find("NumberPanel").GetComponentInChildren<TMP_Text>();
        UserName = transform.Find("NamePanel").GetComponentInChildren<TMP_Text>();
        Score = transform.Find("ScorePanel").GetComponentInChildren<TMP_Text>();
    }

    public void SetUIEnemiesKilledData(string name, int score)
    {
        UserName.text = name;
        this.Score.text = score.ToString();
    }

    public void SetUISurvivalTimeData(string name, float time)
    {
        UserName.text = name;

        float sec = 0;
        float min = 0;
        float hour = 0;
        float t = time;

        while (t > 0)
        {
            min = Mathf.FloorToInt(t / 60);
            sec = Mathf.FloorToInt(t % 60);

            if (min >= 60)
            {
                hour++;
                min -= 60;
                t -= 3600;
            }
            else if (min < 60)
            {
                t -= t;
            }
        }

        score.text = string.Format("{0:00}:{1:00}:{2:00}", hour, min, sec);
    }
}
