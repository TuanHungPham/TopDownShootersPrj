using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplayCtrl : MonoBehaviour
{
    #region public var
    public CharacterData characterData;
    [Space(20)]
    public SpriteRenderer characterSprite;
    public SpriteRenderer characterWeapomSprite;
    public bool IsSelected { get => isSelected; set => isSelected = value; }
    #endregion

    #region private var
    [SerializeField] private Transform selectedPoint;
    [SerializeField] private bool isSelected;
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
        characterData = GetComponentInChildren<CharacterData>();

        characterSprite = GetComponentInChildren<SpriteRenderer>();
        characterWeapomSprite = transform.Find("CharacterWeaponDisplay").GetChild(0).GetComponentInChildren<SpriteRenderer>();
        selectedPoint = transform.root.Find("DisplayPoint").Find("SelectedPoint");
    }

    private void Update()
    {
        DisplayCharacter();
        CheckSelected();
    }

    private void DisplayCharacter()
    {
        if (characterData.IsOwned)
        {
            characterSprite.color = Color.white;
            characterWeapomSprite.color = Color.white;
            return;
        }

        characterSprite.color = Color.black;
        characterWeapomSprite.color = Color.black;
    }

    private void CheckSelected()
    {
        if (transform.position != selectedPoint.position)
        {
            isSelected = false;
            return;
        }

        isSelected = true;
    }
}
