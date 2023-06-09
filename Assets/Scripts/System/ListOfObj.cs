using System.Collections.Generic;
using UnityEngine;

public class ListOfObj : MonoBehaviour
{
    #region public var
    public List<GameObject> listOfObj = new List<GameObject>();
    public GameObject SelectedObj { get => selectedObj; set => selectedObj = value; }
    #endregion

    #region private var
    [SerializeField] private GameObject selectedObj;
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
        InitializeListOfEnemy();
    }

    private void Update()
    {
        GetRandomObjFromList();
    }

    private void InitializeListOfEnemy()
    {
        foreach (Transform item in transform)
        {
            if (listOfObj.Contains(item.gameObject)) continue;

            listOfObj.Add(item.gameObject);
        }
    }

    private void GetRandomObjFromList()
    {
        int index = Random.Range(0, listOfObj.Count);
        SelectedObj = listOfObj[index];
    }
}
