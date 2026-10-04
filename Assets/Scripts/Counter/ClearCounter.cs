using System;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {

            if (player.HasKitchenObject())
            {

                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {

            if (!player.HasKitchenObject())
            {

                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
