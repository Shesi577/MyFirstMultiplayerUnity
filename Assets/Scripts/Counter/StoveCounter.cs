using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private FryReceiptSO[] fryReceiptSOArray;

    private float fryTimer;
    private FryReceiptSO fryReceiptSO;

    private void Update()
    {
        if (HasKitchenObject())
        {
            fryTimer += Time.deltaTime;
            Debug.Log(fryTimer);

            if (fryTimer >= fryReceiptSO.fryingTimerMax)
            {
                fryTimer = 0f;
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {

            if (player.HasKitchenObject())
            {
                if (HasReceiptWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryReceiptSO = fryReceiptSO = GetFryReceiptSOWithInput(GetKitchenObject().GetKitchenObjectSO());
;                }

            }
        }
        else
        {

            if (!player.HasKitchenObject())
            {
                if (HasReceiptWithInput(this.GetKitchenObject().GetKitchenObjectSO()))
                {

                }
                GetKitchenObject().SetKitchenObjectParent(player);
            }

        }

    }

    private bool HasReceiptWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryReceiptSO fryReceiptSO = GetFryReceiptSOWithInput(inputKitchenObjectSO);
        return fryReceiptSO != null;
    }

    private FryReceiptSO GetFryReceiptSOWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        foreach (FryReceiptSO fryReceiptSO in fryReceiptSOArray)
        {
            if (fryReceiptSO.input == inputKitchenObjectSo)
            {
                return fryReceiptSO;
            }
        }
        return null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryReceiptSO fryReceiptSO = GetCuttingReceiptSOWithInput(inputKitchenObjectSO);
        if (fryReceiptSO != null)
        {
            return fryReceiptSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryReceiptSO GetCuttingReceiptSOWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        foreach (FryReceiptSO fryReceiptSO in fryReceiptSOArray)
        {
            if (fryReceiptSO.input == inputKitchenObjectSo)
            {
                return fryReceiptSO;
            }
        }
        return null;
    }
}
