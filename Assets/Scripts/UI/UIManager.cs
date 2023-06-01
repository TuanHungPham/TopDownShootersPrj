using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    [SerializeField] public static UIManager Instance { get => instance; set => instance = value; }
    public AchievementUI AchievementUI { get => achievementUI; set => achievementUI = value; }
    public AmmoSystemUI AmmoSystemUI { get => ammoSystemUI; set => ammoSystemUI = value; }
    public EnemyWaveUI EnemyWaveUI { get => enemyWaveUI; set => enemyWaveUI = value; }
    public GameOverBoard GameOverBoard { get => gameOverBoard; set => gameOverBoard = value; }
    public HitScene HitScene { get => hitScene; set => hitScene = value; }
    public HPBarUI HPBarUI { get => hPBarUI; set => hPBarUI = value; }
    public RespawnSystemUI RespawnSystemUI { get => respawnSystemUI; set => respawnSystemUI = value; }
    public WeaponInventoryPanel WeaponInventoryPanel { get => weaponInventoryPanel; set => weaponInventoryPanel = value; }
    public ShowDamageUIManager ShowDamageUIManager { get => showDamageUIManager; set => showDamageUIManager = value; }

    #region public
    [SerializeField] private AchievementUI achievementUI;
    [SerializeField] private AmmoSystemUI ammoSystemUI;
    [SerializeField] private EnemyWaveUI enemyWaveUI;
    [SerializeField] private GameOverBoard gameOverBoard;
    [SerializeField] private HitScene hitScene;
    [SerializeField] private HPBarUI hPBarUI;
    [SerializeField] private RespawnSystemUI respawnSystemUI;
    [SerializeField] private WeaponInventoryPanel weaponInventoryPanel;
    [SerializeField] private ShowDamageUIManager showDamageUIManager;
    #endregion

    #region private
    #endregion

    private void Awake()
    {
        instance = this;
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        AchievementUI = GetComponentInChildren<AchievementUI>();
        AmmoSystemUI = GetComponentInChildren<AmmoSystemUI>();
        EnemyWaveUI = GetComponentInChildren<EnemyWaveUI>();
        HPBarUI = GetComponentInChildren<HPBarUI>();
        WeaponInventoryPanel = GetComponentInChildren<WeaponInventoryPanel>();
        ShowDamageUIManager = GetComponentInChildren<ShowDamageUIManager>();
        GameOverBoard = transform.Find("GameOverBoard").Find("GameOverAchievementBoard").GetComponent<GameOverBoard>();
        HitScene = transform.Find("HitScene").GetComponent<HitScene>();
        RespawnSystemUI = transform.Find("RespawnBoard").GetComponent<RespawnSystemUI>();
    }
}
