using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TigerForge;

public class AmmoSystemUI : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private TMP_Text ammoCapacityText;
    [SerializeField] private Image grenadeIcon;
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
        playerCtrl = GameObject.Find("------ PLAYER ------").GetComponentInChildren<PlayerCtrl>();
        ammoCapacityText = transform.Find("AmmoCapacityUI").GetComponentInChildren<TMP_Text>();
        grenadeIcon = transform.Find("GrenadeCapacityUIBG").GetChild(0).GetComponent<Image>();
    }

    private void Start()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.PLAYER_SHOOTING.ToString(), ShowAmmo);
        EventManager.StartListening(EventID.SWITCHING_WEAPON.ToString(), ShowAmmo);
        EventManager.StartListening(EventID.PLAYER_THROWING_GRENADE.ToString(), ShowGrenadeQuantity);
    }

    private void ShowAmmo()
    {
        ammoCapacityText.text = playerCtrl.AmmoSystem.CurrentWeaponAmmo.ToString();
    }

    private void ShowGrenadeQuantity()
    {
        grenadeIcon.fillAmount = playerCtrl.GrenadeSystem.GrenadeQuantity / 3f;
    }
}
