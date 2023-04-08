using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponHolderUI : MonoBehaviour, IPointerClickHandler
{
    #region public var
    public HolderType holderType;
    public event Action<WeaponHolderUI> OnItemClicked;
    public bool IsEmpty { get => isEmpty; private set => isEmpty = value; }
    public bool IsSelected { get => isSelected; set => isSelected = value; }
    #endregion

    #region private var
    [SerializeField] private Image background;
    [SerializeField] private Image weaponImage;
    [SerializeField] private bool isEmpty;
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
        background = transform.Find("BackGround").GetComponent<Image>();
        weaponImage = transform.Find("BackGround").Find("WeaponImage").GetComponent<Image>();

        ResetImage();
    }

    public void SetImage(Sprite image)
    {
        weaponImage.gameObject.SetActive(true);
        weaponImage.sprite = image;
        IsEmpty = false;
    }

    public void ResetImage()
    {
        weaponImage.gameObject.SetActive(false);
        weaponImage.sprite = null;
        IsEmpty = true;
    }

    public void Select()
    {
        if (IsEmpty) return;

        Color color;
        ColorUtility.TryParseHtmlString("#225926", out color);

        background.color = color;
        IsSelected = true;
    }

    public void Deselect()
    {
        Color color;
        ColorUtility.TryParseHtmlString("#223B59", out color);

        background.color = color;
        IsSelected = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right) return;

        OnItemClicked?.Invoke(this);
    }
}
