using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{

    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateRemove;

    [SerializeField] private KitchenObjectSO palteKitchenObjectSO;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnAmount;
    private int platesSpawnMax = 4;
    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax)
        {

            spawnPlateTimer = 0f;
            if (platesSpawnAmount < platesSpawnMax)
            {
                platesSpawnAmount++;
                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (platesSpawnAmount > 0)
            {
                platesSpawnAmount--;
                KitchenObject.SpawnKitchentObject(palteKitchenObjectSO, player);
                OnPlateRemove?.Invoke(this, EventArgs.Empty);
            }
        }

    }
}
