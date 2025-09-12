using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Manager References
    private InputManager inputManager;

    // Input Variables
    public Vector2 moveInput;
    public Vector2 lookInput;


    private void Awake()
    {
        inputManager = GameManager.Instance.inputManager;
    }






    #region Input Methods

    void SetMoveInput(Vector2 inputVector)
    {
       moveInput = new Vector2(inputVector.x, inputVector.y);
    }

    void SetLookInput(Vector2 inputVector)
    {
        lookInput = new Vector2(inputVector.x, inputVector.y);
    }


    void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Jump Started");
            // Handle jump start logic here
        }
        else if (context.performed)
        {
            Debug.Log("Jump Performed");
            // Handle jump performed logic here
        }
        else if (context.canceled)
        {
            Debug.Log("Jump Canceled");
            // Handle jump canceled logic here
        }

    }



    #endregion






    void OnEnable()
    {
        inputManager.MoveInputEvent += SetMoveInput;
        inputManager.LookInputEvent += SetLookInput;

        inputManager.JumpInputEvent += HandleJumpInput;


    }

    void OnDestroy()
    {
        inputManager.MoveInputEvent -= SetMoveInput;
        inputManager.LookInputEvent -= SetLookInput;




    }



}
