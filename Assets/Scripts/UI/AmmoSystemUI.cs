using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    private void Update()
    {
        ShowAmmo();
        ShowGrenadeQuantity();
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
