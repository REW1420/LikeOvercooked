using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCouterVisual : MonoBehaviour
{

    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private PlatesCounter platesCounter;
    private List<GameObject> plateVisualGameObjectList;

    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }
    private void Start()
    {
        platesCounter.OnPlateSpawn += PlatesCounter_OnPlateSpawn;
        platesCounter.OnPlateRemove += PlatesCounter_OnPlateRemove;

    }

    private void PlatesCounter_OnPlateSpawn(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        float plateOffsetY = .1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
    private void PlatesCounter_OnPlateRemove(object sender, System.EventArgs e)
    {
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

}
