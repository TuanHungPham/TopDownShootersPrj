using UnityEngine;

public class CharacterDisplayCtrl : MonoBehaviour
{
    #region public var
    public CharacterData characterData;
    [Space(20)]
    public SpriteRenderer characterSprite;
    public SpriteRenderer characterWeapomSprite;
    public int pointIndex;
    public float smoothTime;

    public bool IsSelected { get => isSelected; set => isSelected = value; }
    #endregion

    #region private var
    [SerializeField] private DisplayPointManager displayPointManager;
    [SerializeField] private Transform recentPoint;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private bool isSelected;
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
        characterData = GetComponentInChildren<CharacterData>();
        characterSprite = GetComponentInChildren<SpriteRenderer>();
        displayPointManager = transform.root.GetComponentInChildren<DisplayPointManager>();
        characterWeapomSprite = transform.Find("CharacterWeaponDisplay").GetChild(0).GetComponentInChildren<SpriteRenderer>();
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
        if (characterData.IsOwned)
        {
            characterSprite.color = Color.white;
            characterWeapomSprite.color = Color.white;
            return;
        }

        characterSprite.color = Color.black;
        characterWeapomSprite.color = Color.black;
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
        targetPoint = displayPointManager.listOfDisplayPoint[pointIndex];
        transform.position = Vector3.SmoothDamp(transform.position, targetPoint.position, ref velocity, smoothTime);
    }
}
