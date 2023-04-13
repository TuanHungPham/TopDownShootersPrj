using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get => instance; set => instance = value; }

    public AchievementUI achievementUI;
    public AmmoSystemUI ammoSystemUI;
    public EnemyWaveUI enemyWaveUI;
    public GameOverBoard gameOverBoard;
    public HitScene hitScene;
    public HPBarUI hPBarUI;
    public RespawnSystemUI respawnSystemUI;
    public WeaponInventoryPanel weaponInventoryPanel;

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
        achievementUI = GetComponentInChildren<AchievementUI>();
        ammoSystemUI = GetComponentInChildren<AmmoSystemUI>();
        enemyWaveUI = GetComponentInChildren<EnemyWaveUI>();
        weaponInventoryPanel = GetComponentInChildren<WeaponInventoryPanel>();
        gameOverBoard = transform.Find("GameOverBoard").Find("GameOverAchievementBoard").GetComponent<GameOverBoard>();
        hitScene = transform.Find("HitScene").GetComponent<HitScene>();
        hPBarUI = GetComponentInChildren<HPBarUI>();
        respawnSystemUI = transform.Find("RespawnBoard").GetComponent<RespawnSystemUI>();
    }
}
