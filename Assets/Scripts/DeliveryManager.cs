
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public event System.EventHandler OnRecipeSpawned;
    public event System.EventHandler OnRecipeComplete;
    public static DeliveryManager Instance { get; private set; }
    private List<RecipeSO> waitingRecipeSOList;
    [SerializeField] private RecipeListSO recipeListSO;
    private int waitingRecipeMax = 4;
    private float spawnRecipieTimer;
    private float spawnRecipieTimerMax = 4f;
    private void Awake()
    {
        Instance = this;

        waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Update()
    {

        spawnRecipieTimer -= Time.deltaTime;

        if (spawnRecipieTimer <= 0f)
        {
            if (waitingRecipeSOList.Count < waitingRecipeMax)
            {
                spawnRecipieTimer = spawnRecipieTimerMax;
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

                waitingRecipeSOList.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke(this, System.EventArgs.Empty);
            }

        }
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //has same number of ingredients.
                bool plateContentMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    //cycling through all ingredinets in the recipe
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //cycling through all ingredinets in the plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            //ingredients mathces
                            ingredientFound = true;
                            break;
                        }
                        //scope continue
                        if (!ingredientFound)
                        {
                            //ingredient not in the plate
                            plateContentMatchesRecipe = false;
                        }
                    }
                    if (plateContentMatchesRecipe)
                    {
                        //player delivered correct plate

                        waitingRecipeSOList.RemoveAt(i);
                        OnRecipeComplete?.Invoke(this, System.EventArgs.Empty);
                        return;
                    }
                }
            }
        }

    }


    public List<RecipeSO> GetRecipeSOList()
    {
        return waitingRecipeSOList;
    }

}
