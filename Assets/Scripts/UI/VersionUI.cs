using UnityEngine;
using TMPro;

public class VersionUI : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private TMP_Text versionText;
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
        versionText = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        versionText.text = "Version: " + Application.version;
    }
}
