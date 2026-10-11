using UnityEngine;

[CreateAssetMenu(fileName = "FryReceiptSO", menuName = "Scriptable Objects/FryReceiptSO")]
public class FryReceiptSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float fryingTimerMax;
}
