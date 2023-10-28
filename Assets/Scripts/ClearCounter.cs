using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform ObjectTopPoint;
    private KitchenObject kitchenObject;
    [SerializeField] private ClearCounter secondCounter;
    [SerializeField] private bool testing;

    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenObject != null)
            {
                kitchenObject.SetClearCounter(secondCounter);
            }
        }
    }
    public void Interact()
    {
        if (kitchenObject == null)
        {
            Transform kitchenObjectSOTransform = Instantiate(kitchenObjectSO.prefab, ObjectTopPoint);
            kitchenObjectSOTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        }
        else
        {

        }


    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return ObjectTopPoint;
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
