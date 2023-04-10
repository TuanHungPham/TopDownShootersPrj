using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Assets/Magazine")]
public class MagazineData : ScriptableObject
{
    private int id;
    [SerializeField] private string magazineName;
    [SerializeField] private AmmoType ammoType;
    [SerializeField] private Sprite magazineSprite;
    [SerializeField] private int ammoQuantity;

    public int Id { get => id; set => id = GetInstanceID(); }
    public string MagazineName { get => magazineName; set => magazineName = value; }
    public AmmoType AmmoType { get => ammoType; set => ammoType = value; }
    public Sprite MagazineSprite { get => magazineSprite; set => magazineSprite = value; }
    public int AmmoQuantity { get => ammoQuantity; set => ammoQuantity = value; }
}
