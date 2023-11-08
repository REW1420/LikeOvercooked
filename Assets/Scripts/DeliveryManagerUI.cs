using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;


    private void Start()
    {
        DeliveryManager.Instance.OnRecipeComplete += DeliveryManager_OnRecipeComplete;
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        UpdateVisual();

    }
    private void DeliveryManager_OnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateVisual();

    }

    private void DeliveryManager_OnRecipeComplete(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
           
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}
