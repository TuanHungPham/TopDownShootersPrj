using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using TigerForge;

public class IAPSystem : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private TMP_Text productTitle;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private CodelessIAPButton iAPButton;
    private int coinPackage1;
    private int coinPackage2;
    private int coinPackage3;
    private int coinPackage4;
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

        coinPackage1 = 200;
        coinPackage2 = 1000;
        coinPackage3 = 4000;
        coinPackage4 = 10000;
    }

    public void OnPurchaseComplete(Product product)
    {
        switch (product.definition.id)
        {
            case "coin":
                DataManager.Instance.AchievementDataManager.AddCoin(200);
                break;
            case "alotofcoin":
                DataManager.Instance.AchievementDataManager.AddCoin(1000);
                break;
            case "bagofcoins":
                DataManager.Instance.AchievementDataManager.AddCoin(4000);
                break;
            case "treasure":
                DataManager.Instance.AchievementDataManager.AddCoin(10000);
                break;
            default:
                break;
        }

        EventManager.EmitEvent(EventID.CHANGING_COIN_QUANTITY.ToString());
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
