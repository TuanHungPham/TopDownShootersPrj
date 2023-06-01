using UnityEngine;

public abstract class Status : MonoBehaviour
{
    #region public var
    public bool IsDeath { get => isDeath; set => isDeath = value; }
    public float CurrentHP { get => currentHP; set => currentHP = value; }
    public float MaxHP { get => maxHP; set => maxHP = value; }
    #endregion

    #region private var
    [SerializeField] private float currentHP;
    [SerializeField] private float maxHP;
    [SerializeField] private bool isDeath;
    #endregion

    protected virtual void Awake()
    {
        LoadComponents();
    }

    protected virtual void Reset()
    {
        LoadComponents();
    }

    protected abstract void LoadComponents();

    protected virtual void Update()
    {
        CheckHP();
    }

    protected virtual void CheckHP()
    {
        if (CurrentHP <= 0)
        {
            CurrentHP = 0;
            Die();
        }
    }

    protected virtual void Die()
    {
        isDeath = true;
        DisableComponents();
    }

    public virtual void Heal(int healAmount)
    {
        CurrentHP += healAmount;

        if (CurrentHP < MaxHP) return;

        CurrentHP = MaxHP;
    }

    protected abstract void DisableComponents();
}
