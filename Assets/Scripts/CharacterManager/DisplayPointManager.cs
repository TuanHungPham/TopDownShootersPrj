using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPointManager : MonoBehaviour
{
    #region public var
    public List<Transform> listOfDisplayPoint = new List<Transform>();
    #endregion

    #region private var
    [SerializeField] private CharacterManagerCtrl characterManagerCtrl;
    #endregion

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void Start()
    {
        InitializePointForCharacter();
    }

    private void LoadComponents()
    {
        characterManagerCtrl = transform.parent.GetComponentInChildren<CharacterManagerCtrl>();

        InitializeDisplayPointList();
    }

    private void InitializeDisplayPointList()
    {
        foreach (Transform point in transform)
        {
            if (listOfDisplayPoint.Contains(point)) return;

            listOfDisplayPoint.Add(point);
        }
    }

    private void InitializePointForCharacter()
    {
        for (int i = 0; i < characterManagerCtrl.listOfCharacter.Count; i++)
        {
            CharacterDisplayCtrl characterDisplayCtrl = characterManagerCtrl.listOfCharacter[i].GetComponent<CharacterDisplayCtrl>();

            characterDisplayCtrl.pointIndex = i;
        }
    }

    public void SwitchRight()
    {
        for (int i = 0; i < characterManagerCtrl.listOfCharacter.Count; i++)
        {
            CharacterDisplayCtrl characterDisplayCtrl = characterManagerCtrl.listOfCharacter[i].GetComponent<CharacterDisplayCtrl>();

            int index = characterDisplayCtrl.pointIndex;
            index++;

            if (index > listOfDisplayPoint.Count - 1)
            {
                index = 0;
            }

            characterDisplayCtrl.pointIndex = index;
        }
    }

    public void SwitchLeft()
    {
        for (int i = 0; i < characterManagerCtrl.listOfCharacter.Count; i++)
        {
            CharacterDisplayCtrl characterDisplayCtrl = characterManagerCtrl.listOfCharacter[i].GetComponent<CharacterDisplayCtrl>();

            int index = characterDisplayCtrl.pointIndex;
            index--;

            if (index < 0)
            {
                index = listOfDisplayPoint.Count - 1;
            }

            characterDisplayCtrl.pointIndex = index;
        }
    }
}
