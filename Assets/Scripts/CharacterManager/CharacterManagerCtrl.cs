using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManagerCtrl : MonoBehaviour
{
    public CharacterShop characterShop;

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

    }
}
