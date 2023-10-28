using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour

{

    public event EventHandler OnInteractAction;
    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interaction.performed += Interact_perfomed;
    }

    private void Interact_perfomed(InputAction.CallbackContext context)
    {

        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();



        inputVector = inputVector.normalized;
        return inputVector;
    }

    public bool IsRunning()
    {
        bool isShiftCurrentlyPressed = Input.GetKey(KeyCode.LeftShift);


        return isShiftCurrentlyPressed;
    }

}
