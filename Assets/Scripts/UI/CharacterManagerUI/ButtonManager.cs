using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    #region public var

    #endregion

    #region private var
    [SerializeField] private CharacterManagerCtrl characterManagerCtrl;
    [SerializeField] private Transform levelUpButton;
    [SerializeField] private Transform buyButton;
    [SerializeField] private TMP_Text coinRequirementText;
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
        characterManagerCtrl = GameObject.Find("------ MANAGER ------").GetComponentInChildren<CharacterManagerCtrl>();

        levelUpButton = transform.Find("LevelUpButton");
        buyButton = transform.Find("BuyButton");
        coinRequirementText = transform.Find("CointRequirement").GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        HandleButton();
        ShowCoinRequirement();
    }

    private void HandleButton()
    {
        if (!characterManagerCtrl.characterShop.IsCharacterCanBeOwned)
        {
            levelUpButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);
            return;
        }

        buyButton.gameObject.SetActive(true);
        levelUpButton.gameObject.SetActive(false);
    }

    private void ShowCoinRequirement()
    {
        if (characterManagerCtrl.selectedCharacter == null) return;

        CharacterDisplayCtrl characterDisplayCtrl = characterManagerCtrl.selectedCharacter.GetComponent<CharacterDisplayCtrl>();

        coinRequirementText.text = characterDisplayCtrl.characterData.coinRequirement.ToString();
    }
}
