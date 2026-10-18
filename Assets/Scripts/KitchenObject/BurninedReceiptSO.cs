using UnityEngine;

[CreateAssetMenu(fileName = "BurninedReceiptSO", menuName = "Scriptable Objects/BurninedReceiptSO")]
public class BurninedReceiptSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float burningTimerMax;
}
