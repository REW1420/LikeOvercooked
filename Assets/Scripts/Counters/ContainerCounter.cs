using System;

using UnityEngine;


public class ContainerCounter : BaseCounter, IKitchenObjectParent
{
    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;



    public override void Interact(Player player)
    {


        if (!player.HasKitchenObject())
        {
            if (!HasKitchenObject())
            {

                KitchenObject.SpawnKitchentObject(kitchenObjectSO, player);
                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            }
        }
    }

}
