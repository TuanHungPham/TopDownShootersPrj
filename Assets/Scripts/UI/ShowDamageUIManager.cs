using TMPro;
using UnityEngine;

public class ShowDamageUIManager : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private GameObject damageTextPrefab;
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

    }

    public void ShowDamage(Vector3 showPosition, int damage)
    {
        GameObject damageText = Instantiate(damageTextPrefab);
        damageText.transform.position = showPosition;
        damageText.transform.SetParent(transform);
        damageText.transform.localScale = Vector3.one;

        SetDamageText(damageText, damage);
    }

    private void SetDamageText(GameObject damageText, int damage)
    {
        TMP_Text text = damageText.GetComponentInChildren<TMP_Text>();
        if (text == null) return;

        text.text = damage.ToString();
    }
}
