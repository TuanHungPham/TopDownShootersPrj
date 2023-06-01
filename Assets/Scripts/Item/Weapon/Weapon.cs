using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region public var
    public WeaponData WeaponData { get => weaponData; set => weaponData = value; }
    #endregion

    #region private var
    [SerializeField] private WeaponData weaponData;
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

        weaponSprite.sprite = WeaponData.WeaponSprite;
    }
}
