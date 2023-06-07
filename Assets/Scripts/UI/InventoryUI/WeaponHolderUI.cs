using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponHolderUI : MonoBehaviour, IPointerClickHandler
{
    #region public var
    public event Action<WeaponHolderUI> OnItemClicked;
    public bool IsEmpty { get => isEmpty; set => isEmpty = value; }
    public bool IsSelected { get => isSelected; set => isSelected = value; }
    public HolderType HolderType { get => holderType; set => holderType = value; }
    #endregion

    #region private var
    [SerializeField] private Image background;
    [SerializeField] private Image weaponImage;
    [SerializeField] private Sprite noneWeaponImage;
    [SerializeField] private HolderType holderType;
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
    }

    private void Start()
    {
        ResetImage();
    }

    public void SetImage(Sprite image)
    {
        weaponImage.sprite = image;
        weaponImage.color = Color.white;
        IsEmpty = false;
    }

    public void ResetImage()
    {
        weaponImage.sprite = noneWeaponImage;
        weaponImage.color = Color.red;
        IsEmpty = true;
    }

    public void Select()
    {
        if (IsEmpty) return;

        background.color = SetColor("#225926");
        IsSelected = true;
    }

    public void Deselect()
    {
        background.color = SetColor("#223B59");
        IsSelected = false;
    }

    private Color SetColor(string colorString)
    {
        Color color;
        ColorUtility.TryParseHtmlString(colorString, out color);

        return color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right) return;

        OnItemClicked?.Invoke(this);
    }
}
