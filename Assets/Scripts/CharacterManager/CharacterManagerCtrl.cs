using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManagerCtrl : MonoBehaviour
{
    public CharacterShop characterShop;
    public CharacterUpgrade characterUpgrade;

    private void Awake()
    {
        LoadComponents();

        // SetDefaultCharacter();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        characterShop = GetComponent<CharacterShop>();
        characterUpgrade = GetComponent<CharacterUpgrade>();
    }

    private void SetDefaultCharacter()
    {
        foreach (Transform character in transform)
        {
            if (!character.name.Equals("Character1")) continue;

            CharacterDisplayCtrl characterDisplayCtrl = character.GetComponent<CharacterDisplayCtrl>();
            characterDisplayCtrl.characterData.IsOwned = true;
        }
    }
}
