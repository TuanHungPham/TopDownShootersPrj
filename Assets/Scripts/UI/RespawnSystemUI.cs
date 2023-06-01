using TMPro;
using UnityEngine;

public class RespawnSystemUI : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private TMP_Text respawnTimerText;
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
        respawnTimerText = transform.Find("RespawnTimerUI").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        ShowRespawnTimerText();
    }

    private void ShowRespawnTimerText()
    {
        float sec = Mathf.FloorToInt(InGameManager.Instance.RespawnManager.RespawnAvailableTimer % 60);
        respawnTimerText.text = sec.ToString();
    }
}
