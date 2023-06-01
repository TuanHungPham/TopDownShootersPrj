using UnityEngine;

public class HitScene : MonoBehaviour
{
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private float enableTime;

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
        playerCtrl = GameObject.Find("------ PLAYER ------").transform.Find("MainCharacter").GetComponent<PlayerCtrl>();
    }

    private void Update()
    {
        Invoke("DisableHitScene", enableTime);
    }

    public void TriggerHitScene()
    {
        this.gameObject.SetActive(true);
    }

    private void DisableHitScene()
    {
        this.gameObject.SetActive(false);
    }
}
