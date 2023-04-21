using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    private void Reset()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        scoreText.text = "Score: " + TestScore.Instance.score.ToString();
    }
}
