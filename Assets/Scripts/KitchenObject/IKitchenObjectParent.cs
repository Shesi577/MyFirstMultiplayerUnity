using UnityEngine;
using UnityEngine.UIElements;

public interface IKitchenObjectParent
{
    public Transform GetKitchenObjectFollowTransform();

    public void ClearKitchenObject();

    public void SetKitchenObject(KitchenObject kitchenObject);

    public KitchenObject GetKitchenObject();

    public bool HasKitchenObject();
}
