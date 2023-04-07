using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region public var
    public WeaponData weaponData;
    #endregion

    #region private var
    [SerializeField] private SpriteRenderer weaponSprite;
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
        weaponSprite = GetComponentInChildren<SpriteRenderer>();

        weaponSprite.sprite = weaponData.WeaponSprite;
    }
}
