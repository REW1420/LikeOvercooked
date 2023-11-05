using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour

{

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interaction.performed += Interact_perfomed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_perfomed;
    }

    private void InteractAlternate_perfomed(InputAction.CallbackContext context)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_perfomed(InputAction.CallbackContext context)
    {
        Debug.Log("interaccion");

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
