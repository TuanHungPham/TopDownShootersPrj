using UnityEngine;

public class CharacterDisplayCtrl : MonoBehaviour
{
    #region public var
    public bool IsSelected { get => isSelected; set => isSelected = value; }
    public int PointIndex { get => pointIndex; set => pointIndex = value; }
    public CharacterData CharacterData { get => characterData; set => characterData = value; }
    #endregion

    #region private var
    [Space(20)]
    [SerializeField] private int pointIndex;
    [SerializeField] private float smoothTime;
    [SerializeField] private bool isSelected;

    [Space(20)]
    [SerializeField] private Transform recentPoint;
    [SerializeField] private Transform targetPoint;

    [Space(20)]
    [SerializeField] private SpriteRenderer characterSprite;
    [SerializeField] private SpriteRenderer characterWeaponSprite;
    [SerializeField] private DisplayPointManager displayPointManager;
    [SerializeField] private CharacterData characterData;
    private Transform selectedPoint;
    private Vector3 velocity = Vector3.zero;
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
        CharacterData = GetComponentInChildren<CharacterData>();
        characterSprite = GetComponentInChildren<SpriteRenderer>();
        displayPointManager = transform.root.GetComponentInChildren<DisplayPointManager>();
        characterWeaponSprite = transform.Find("CharacterWeaponDisplay").GetChild(0).GetComponentInChildren<SpriteRenderer>();
        selectedPoint = transform.root.Find("DisplayPoint").Find("SelectedPoint");
    }

    private void Update()
    {
        DisplayCharacter();
        CheckSelected();
        MoveToRecentPoint();
    }

    private void DisplayCharacter()
    {
        if (CharacterData.IsOwned)
        {
            characterSprite.color = Color.white;
            characterWeaponSprite.color = Color.white;
            return;
        }

        characterSprite.color = Color.black;
        characterWeaponSprite.color = Color.black;
    }

    private void CheckSelected()
    {
        if (targetPoint == selectedPoint)
        {
            isSelected = true;
            return;
        }

        isSelected = false;
    }

    private void MoveToRecentPoint()
    {
        targetPoint = displayPointManager.listOfDisplayPoint[PointIndex];
        transform.position = Vector3.SmoothDamp(transform.position, targetPoint.position, ref velocity, smoothTime);
    }
}
