using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
    [SerializeField] private CuttingReceiptSO[] cuttingReceiptSOArray;

    private int cuttingProgress;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {

            if (player.HasKitchenObject())
            {
                if (HasReceiptWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    CuttingReceiptSO cuttingReceiptSO = GetCuttingReceiptSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { progressNormalized = (float)cuttingProgress / cuttingReceiptSO.cuttingprogressMax });
                }


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

    public override void InteractAltarnate(Player player)
    {
        if (HasKitchenObject() && HasReceiptWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;
            CuttingReceiptSO cuttingReceiptSO = GetCuttingReceiptSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { progressNormalized = (float)cuttingProgress / cuttingReceiptSO.cuttingprogressMax });
            if (cuttingProgress >= cuttingReceiptSO.cuttingprogressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObjectParent(outputKitchenObjectSO, this);
            }
        }
    }

    private bool HasReceiptWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingReceiptSO cuttingReceiptSO = GetCuttingReceiptSOWithInput(inputKitchenObjectSO);
        return cuttingReceiptSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingReceiptSO cuttingReceiptSO = GetCuttingReceiptSOWithInput(inputKitchenObjectSO);
        if (cuttingReceiptSO != null)
        {
            return cuttingReceiptSO.output;
        }
        else
        {
            return null;
        }
    }

    private CuttingReceiptSO GetCuttingReceiptSOWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        foreach (CuttingReceiptSO cuttingReceiptSO in cuttingReceiptSOArray)
        {
            return cuttingReceiptSO;
        }
        return null;
    }
}
