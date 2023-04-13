using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Assets/Weapon")]
public class WeaponData : ScriptableObject
{
    private int id;
    [SerializeField] private string weaponName;
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private Sprite weaponSprite;
    [SerializeField] private int weaponDmg;
    [SerializeField] private int shootDistance;
    [SerializeField] private float fireRate;
    [SerializeField] private AmmoType ammoType;
    [SerializeField] private int initialAmmo;
    [SerializeField] private int ammo;

    public int Id { get => id; set => id = GetInstanceID(); }
    public string WeaponName { get => weaponName; set => weaponName = value; }
    public Sprite WeaponSprite { get => weaponSprite; set => weaponSprite = value; }
    public int WeaponDmg { get => weaponDmg; set => weaponDmg = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public int ShootDistance { get => shootDistance; set => shootDistance = value; }
    public WeaponType WeaponType { get => weaponType; set => weaponType = value; }
    public AmmoType AmmoType { get => ammoType; set => ammoType = value; }
    public int InitialAmmo { get => initialAmmo; set => initialAmmo = value; }
    public int Ammo { get => ammo; set => ammo = value; }
}
