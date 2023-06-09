using TMPro;
using UnityEngine;

public class EnemyWaveUI : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private TMP_Text numberOfEnemy;
    [SerializeField] private TMP_Text waveNumber;
    [SerializeField] private TMP_Text nextWaveTimer;
    [SerializeField] private GameObject waveNumerPanel;
    [SerializeField] private GameObject nextWavePanel;
    private float min, sec;
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
        numberOfEnemy = transform.Find("NumberOfEnemy").GetComponentInChildren<TMP_Text>();
        waveNumerPanel = transform.Find("WaveNumber").gameObject;
        nextWavePanel = transform.Find("NextWaveNotification").gameObject;
        waveNumber = waveNumerPanel.transform.Find("Number").GetComponent<TMP_Text>();
        nextWaveTimer = nextWavePanel.transform.Find("Number").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        ShowNumberOfEnemy();
        ShowWaveNumber();
        ShowNextWaveTimer();
        SetWavePanel();
    }

    private void ShowNumberOfEnemy()
    {
        numberOfEnemy.text = EnemyWaveManager.Instance.RestOfEnemy.ToString();
    }

    private void ShowWaveNumber()
    {
        waveNumber.text = EnemyWaveManager.Instance.WaveNumber.ToString();
    }

    private void ShowNextWaveTimer()
    {
        min = Mathf.FloorToInt(EnemyWaveManager.Instance.NextWaveTimer / 60);
        sec = Mathf.FloorToInt(EnemyWaveManager.Instance.NextWaveTimer % 60);

        nextWaveTimer.text = string.Format("{0:00}:{1:00}", min, sec);
    }

    private void SetWavePanel()
    {
        if (!EnemyWaveManager.Instance.UpdateWave)
        {
            waveNumerPanel.gameObject.SetActive(true);
            nextWavePanel.gameObject.SetActive(false);
            return;
        }

        waveNumerPanel.gameObject.SetActive(false);
        nextWavePanel.gameObject.SetActive(true);
    }
}
