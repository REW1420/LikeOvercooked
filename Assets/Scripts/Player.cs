using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IKitchenObjectParent
{


    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    private bool isWalking;
    private Vector3 lastInteractionDir;
    private BaseCounter selectedCounter;
    [SerializeField] private Transform KitchenObjectHoldPoint;
    [SerializeField] private KitchenObject kitchenObject;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("more than one instance");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteraction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;

    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteraction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleInteractions();

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
        if (Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
                else
                {
                    SetSelectedCounter(null);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }

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
            canMove = moverDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHigh, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moverDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moverDir.z).normalized;
                canMove = moverDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHigh, playerRadius, moveDirZ, moveDistance);

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

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return KitchenObjectHoldPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}

