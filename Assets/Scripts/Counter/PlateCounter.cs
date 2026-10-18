using System;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateSpawn;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private float spawnFlateTimerMax = 4f;

    private int plateAmount;
    private int plateAmountMax = 5;

    private void Start()
    {
        plateAmount = 0;
    }

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        
        if (spawnPlateTimer > spawnFlateTimerMax)
        {
                spawnPlateTimer = 0f;
            if (plateAmount < plateAmountMax)
            {
                plateAmount++;
                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
