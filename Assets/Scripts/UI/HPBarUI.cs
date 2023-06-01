using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    #region public var
    public Slider HpSlider { get => hpSlider; set => hpSlider = value; }
    #endregion

    #region private var
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider hpBGSlider;
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private float smoothTime;
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
        hpText = transform.Find("TextBG").GetComponentInChildren<TMP_Text>();
        HpSlider = transform.Find("HPBarBG").Find("HPSlider").GetComponent<Slider>();
        hpBGSlider = transform.Find("HPBarBG").Find("HPBGSlider").GetComponent<Slider>();

        playerCtrl = GameObject.Find("------ PLAYER ------").transform.Find("MainCharacter").GetComponent<PlayerCtrl>();

        smoothTime = 0.5f;
    }

    public void InitializeHPBar()
    {
        HpSlider.maxValue = playerCtrl.PlayerStatus.MaxHP;
        HpSlider.value = playerCtrl.PlayerStatus.CurrentHP;
        hpBGSlider.maxValue = HpSlider.maxValue;
        hpBGSlider.value = HpSlider.value;
    }

    private void Update()
    {
        ShowHP();
    }

    private void ShowHP()
    {
        hpText.text = string.Format("{0}/{1}", playerCtrl.PlayerStatus.CurrentHP, playerCtrl.PlayerStatus.MaxHP);

        SetHPSlider();
    }

    private void SetHPSlider()
    {
        if (HpSlider.value == playerCtrl.PlayerStatus.CurrentHP) return;

        if (HpSlider.value < playerCtrl.PlayerStatus.CurrentHP)
        {
            HpSlider.value += smoothTime;
            Invoke("SetHPBGSlider", 0.4f);
        }
        else
        {
            HpSlider.value -= smoothTime;
            Invoke("SetHPBGSlider", 0.4f);
        }
    }

    private void SetHPBGSlider()
    {
        if (hpBGSlider.value == HpSlider.value) return;

        if (hpBGSlider.value < HpSlider.value)
        {
            hpBGSlider.value += smoothTime;
        }
        else
        {
            hpBGSlider.value -= smoothTime;
        }
    }
}
