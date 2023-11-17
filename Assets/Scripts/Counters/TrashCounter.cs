using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectTrashed;
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySeflt();
            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);

        }
    }
    new public static void ResetStatisData()
    {
        OnAnyObjectTrashed = null;
    }
}
