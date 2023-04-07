using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoSystemUI : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private TMP_Text ammoCapacityText;
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
    }

    private void Update()
    {
        ShowAmmo();
    }

    private void ShowAmmo()
    {
        ammoCapacityText.text = playerCtrl.ammoSystem.currentWeaponAmmo.ToString();
    }
}
