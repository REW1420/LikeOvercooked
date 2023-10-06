using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;
    private Vector3 lastInteractionDir;

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
        gameInput.IsRunning();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        float interactDistance = 2f;
        Vector3 moveDir = new(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractionDir = moveDir;
        }
        if (Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit raycastHit, interactDistance))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }
        else
        {

        }
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();




        Vector3 moverDir = new(inputVector.x, 0f, inputVector.y);
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHigh = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHigh, playerRadius, moverDir, moveDistance);
        if (!canMove)
        {
            //can moce that direction
            //only x movement
            Vector3 moveDirX = new Vector3(moverDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHigh, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moverDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moverDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHigh, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moverDir = moveDirZ;
                }
                else
                {

                }
            }
        }
        if (canMove)
        {
            transform.position += moveDistance * moverDir;
        }


        isWalking = moverDir != Vector3.zero;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moverDir, Time.deltaTime * rotateSpeed);
    }
}
