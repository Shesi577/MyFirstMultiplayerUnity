using UnityEngine;

[CreateAssetMenu(fileName = "CuttingReceiptSO", menuName = "Scriptable Objects/CuttingReceiptSO")]
public class CuttingReceiptSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int cuttingprogressMax;
}
