using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;




    public override void Interact(Player player)
    {

        if (!HasKitchenObject())
        {
            //theres no objecte here
            if (player.HasKitchenObject())
            {
                //player carry shometinh
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {//player not carry shometinh

            }
        }
        else
        {
            //there a object here
            if (player.HasKitchenObject())
            {
                //player is caring something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //is a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySeflt();
                    }
                }
                else
                {
                    //player is holding something else but no plate
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //counter is holding a plate

                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySeflt();
                        }
                    }


                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    }


}



