using TMPro;
using UnityEngine;

public class CharacterManagerUI : MonoBehaviour
{
    private static CharacterManagerUI instance;
    public static CharacterManagerUI Instance { get => instance; private set => instance = value; }

    #region public var
    #endregion

    #region private var
    [SerializeField] private ButtonManager buttonManager;
    [SerializeField] private CharacterInfoPanel characterInfoPanel;
    [SerializeField] private CharacterManagerCtrl characterManagerCtrl;
    [SerializeField] private TMP_Text usernameText;

    #endregion

    private void Awake()
    {
        instance = this;
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void Start()
    {
        ShowUsername();
    }

    private void LoadComponents()
    {
        usernameText = transform.Find("UsernameUI").GetComponent<TMP_Text>();
        buttonManager = GetComponentInChildren<ButtonManager>();
        characterInfoPanel = GetComponentInChildren<CharacterInfoPanel>();

        characterManagerCtrl = GameObject.Find("------ MANAGER ------").GetComponentInChildren<CharacterManagerCtrl>();
    }

    public void ShowUsername()
    {
        usernameText.text = UserManager.Instance.UserName;
    }
}
