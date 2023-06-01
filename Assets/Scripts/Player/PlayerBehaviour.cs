using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    #region public var
    #endregion

    #region private var
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerCtrl playerCtrl;
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
        animator = GetComponent<Animator>();
        playerCtrl = GameObject.Find("------ PLAYER ------").GetComponentInChildren<PlayerCtrl>();
    }

    private void Update()
    {
        SetRunAnimation();
        SetDeathAnimation();
    }

    private void SetRunAnimation()
    {
        if (playerCtrl.PlayerMovement.Joystick.Horizontal != 0 || playerCtrl.PlayerMovement.Joystick.Vertical != 0 || InputManager.Instance.IsMovingInput)
        {
            animator.SetBool("Run", true);
            playerCtrl.PlayerSound.SetMovingSound(true);
            return;
        }
        playerCtrl.PlayerSound.SetMovingSound(false);
        animator.SetBool("Run", false);
    }

    private void SetDeathAnimation()
    {
        if (playerCtrl.PlayerStatus.IsDeath)
        {
            animator.SetBool("isDeath", true);
            return;
        }

        animator.SetBool("isDeath", false);
    }
}
