using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class IAPSystem : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private TMP_Text productTitle;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private CodelessIAPButton iAPButton;
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
        iAPButton = GetComponent<CodelessIAPButton>();
        productTitle = transform.GetChild(0).Find("ProductTitle").GetComponent<TMP_Text>();
        priceText = transform.GetChild(0).Find("Button").GetComponentInChildren<TMP_Text>();

        iAPButton.button = transform.GetChild(0).Find("Button").GetComponent<Button>();
    }

    public void OnPurchaseComplete(Product product)
    {
        Debug.Log("You have just bought " + product.metadata.localizedTitle);
        switch (product.definition.id)
        {
            case "coin":
                UserManager.Instance.Coin += 200;
                Debug.Log("Add 200 Coin to your Account!!!");
                break;
            case "alotofcoin":
                UserManager.Instance.Coin += 1000;
                break;
            case "bagofcoins":
                UserManager.Instance.Coin += 4000;
                break;
            case "treasure":
                UserManager.Instance.Coin += 10000;
                break;
            default:
                break;
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription purchaseFailureDescription)
    {
        Debug.LogWarning(purchaseFailureDescription.message);
    }

    public void OnProductFetched(Product product)
    {
        Debug.Log("----------------ID: " + product.definition.id);
        Debug.Log("----------------Title: " + product.metadata.localizedTitle);
        priceText.text = product.metadata.localizedPriceString;
    }
}
