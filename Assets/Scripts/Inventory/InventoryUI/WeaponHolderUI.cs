using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponHolderUI : MonoBehaviour, IPointerClickHandler
{
    #region public var
    public event Action<WeaponHolderUI> OnItemClicked;
    #endregion

    #region private var
    [SerializeField] private Image background;
    [SerializeField] private Image weaponImage;
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
    }

    public void ResetImage()
    {
        weaponImage.gameObject.SetActive(false);
        weaponImage.sprite = null;
    }

    public void Select()
    {
        Color color;
        ColorUtility.TryParseHtmlString("#225926", out color);

        background.color = color;
    }

    public void Deselect()
    {
        Color color;
        ColorUtility.TryParseHtmlString("#223B59", out color);

        background.color = color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right) return;

        OnItemClicked?.Invoke(this);
    }
}
