using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> plateVisualGameObjectList;

    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlateSpawn += PlatesCounter_OnPlatesSpawned;
    }

    private void PlatesCounter_OnPlatesSpawned(object sender, EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOfSetY = .1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOfSetY * plateVisualGameObjectList.Count, 0);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
