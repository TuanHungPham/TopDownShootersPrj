using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    public static InputManager Instance { get => instance; set => instance = value; }

    #region public var
    public bool IsMovingInput { get => isMovingInput; set => isMovingInput = value; }
    #endregion

    #region private var
    [SerializeField] private bool isMovingInput;
    #endregion

    private void Awake()
    {
        instance = this;

    }

    private void Update()
    {
        GetMovingInput();
    }

    private void GetMovingInput()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            IsMovingInput = true;
            return;
        }
        IsMovingInput = false;
    }
}
