using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{


    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }
    [SerializeField] private FryingRecipieSO[] fryingRecipieSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipieSOArray;

    private State state;
    private FryingRecipieSO fryingRecipieSO;
    private BurningRecipeSO burningRecipeSO;
    private float fryringTimer = 0f;
    private float burningTimer = 0f;

    private void Start()
    {
        state = State.Idle;

    }
    private void Update()
    {

        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:

                    break;
                case State.Frying:

                    fryringTimer += Time.deltaTime;


                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryringTimer / fryingRecipieSO.fryingTimeMax
                    });
                    if (fryringTimer > fryingRecipieSO.fryingTimeMax)
                    {

                        GetKitchenObject().DestroySeflt();

                        KitchenObject.SpawnKitchentObject(fryingRecipieSO.output, this);
                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });


                    }

                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimeMax
                    });
                    if (burningTimer > burningRecipeSO.burningTimeMax)
                    {

                        GetKitchenObject().DestroySeflt();

                        KitchenObject.SpawnKitchentObject(burningRecipeSO.output, this);
                        state = State.Burned;
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                    }

                    break;
                case State.Burned:

                    break;
            }
        }

    }
    public override void Interact(Player player)
    {

        if (!HasKitchenObject())
        {
            //theres nothing in the counter
            if (player.HasKitchenObject())
            {
                //player carry something

                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player carry something fryied
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipieSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;
                    fryringTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryringTimer / fryingRecipieSO.fryingTimeMax
                    });

                }
            }
            else
            {
                //player not carry a thing

            }
        }
        else
        {
            //theres a object in the counter
            if (player.HasKitchenObject())
            {
                //player carry something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //is a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySeflt();
                        state = State.Idle;
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                    }
                }
            }
            else
            {
                //player not carry anythig
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });
            }
        }

    }
    public bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipieSO fryingRecipieSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        return fryingRecipieSO != null;
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipieSO fryingRecipieSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        if (fryingRecipieSO != null)
        {
            return fryingRecipieSO.output;
        }
        else
        {
            return null;
        }
    }
    private FryingRecipieSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipieSO fryingRecipieSO in fryingRecipieSOArray)
        {
            if (fryingRecipieSO.input == inputKitchenObjectSO)
            {
                return fryingRecipieSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipieSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }
}
