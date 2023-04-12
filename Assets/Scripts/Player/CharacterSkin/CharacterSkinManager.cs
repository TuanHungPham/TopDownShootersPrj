using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkinManager : MonoBehaviour
{
    #region public var
    public List<Transform> listOfCharacterSkin = new List<Transform>();
    #endregion

    #region private var
    [SerializeField] private Transform recentCharacterSkin;
    [SerializeField] private Transform selectedCharacterSkin;
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
        recentCharacterSkin = GameObject.Find("------ PLAYER ------").transform.GetChild(0).Find("CharacterSprite");

        InitializeCharacterSkinList();
    }

    private void Update()
    {
        ChangeSkin();
    }

    private void InitializeCharacterSkinList()
    {
        foreach (Transform skin in transform)
        {
            if (listOfCharacterSkin.Contains(skin)) continue;

            listOfCharacterSkin.Add(skin);
        }
    }

    private void ChangeSkin()
    {
        if (selectedCharacterSkin == null) return;

        Animator animator = recentCharacterSkin.GetComponent<Animator>();
        Animator newAnimator = selectedCharacterSkin.GetComponent<Animator>();
        animator.runtimeAnimatorController = newAnimator.runtimeAnimatorController;

        selectedCharacterSkin = null;
    }

    public void GetSkin(int index)
    {
        selectedCharacterSkin = listOfCharacterSkin[index];
    }

    public void GetSkin1()
    {
        selectedCharacterSkin = listOfCharacterSkin.Find((x) => x.name.Equals("Character1Sprite"));
    }
    public void GetSkin2()
    {
        selectedCharacterSkin = listOfCharacterSkin.Find((x) => x.name.Equals("Character2Sprite"));
    }
    public void GetSkin3()
    {
        selectedCharacterSkin = listOfCharacterSkin.Find((x) => x.name.Equals("Character3Sprite"));
    }
    public void GetSkin4()
    {
        selectedCharacterSkin = listOfCharacterSkin.Find((x) => x.name.Equals("Character4Sprite"));
    }
}
