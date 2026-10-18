using System;
using UnityEngine;
using static IHasProgress;

public class StoveCounter : BaseCounter, IKitchenObjectParent, IHasProgress
{
    public EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private FryReceiptSO[] fryReceiptSOArray;
    [SerializeField] private BurninedReceiptSO[] burningReceiptSOArray;
    private State state;

    private float fryTimer;
    private float burningTimer;
    private FryReceiptSO fryReceiptSO;
    private BurninedReceiptSO burningReceiptSO;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    private void Start()
    {
        state = State.Idle;

        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
        {
            state = state
        });
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
                    fryTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { progressNormalized = (float) fryTimer / fryReceiptSO.fryingTimerMax });

                    if (fryTimer >= fryReceiptSO.fryingTimerMax)
                    {
                        //Fried
                        fryTimer = 0f;

                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObjectParent(fryReceiptSO.output, this);
                        burningReceiptSO = GetBurningReceiptSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        Debug.Log(burningReceiptSO);
                        state = State.Fried;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { progressNormalized = 0f });

                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { progressNormalized = (float)burningTimer / burningReceiptSO.burningTimerMax });


                    if (burningTimer >= burningReceiptSO.burningTimerMax)
                    {
                        //Burned
                        burningTimer = 0f;
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObjectParent(burningReceiptSO.output, this);
                        state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { progressNormalized = 0f });
                    }
                    break;
                case State.Burned:
                    break;
            }
            Debug.Log(state);
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {

            if (player.HasKitchenObject())
            {
                if (HasReceiptWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryReceiptSO = fryReceiptSO = GetFryReceiptSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    fryTimer = 0f;
                    burningTimer = 0f;
                    state = State.Frying;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { progressNormalized = (float)fryTimer / fryReceiptSO.fryingTimerMax });

                    
                }

            }
        }
        else
        {

            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { progressNormalized = 0f });
            }

        }

    }

    private bool HasReceiptWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryReceiptSO fryReceiptSO = GetFryReceiptSOWithInput(inputKitchenObjectSO);
        return fryReceiptSO != null;
    }

    private FryReceiptSO GetFryReceiptSOWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        foreach (FryReceiptSO fryReceiptSO in fryReceiptSOArray)
        {
            if (fryReceiptSO.input == inputKitchenObjectSo)
            {
                return fryReceiptSO;
            }
        }
        return null;
    }

    private BurninedReceiptSO GetBurningReceiptSOWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        foreach (BurninedReceiptSO burningReceiptSO in burningReceiptSOArray)
        {
            if (burningReceiptSO.input == inputKitchenObjectSo)
            {
                return burningReceiptSO;
            }
        }
        return null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryReceiptSO fryReceiptSO = GetCuttingReceiptSOWithInput(inputKitchenObjectSO);
        if (fryReceiptSO != null)
        {
            return fryReceiptSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryReceiptSO GetCuttingReceiptSOWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        foreach (FryReceiptSO fryReceiptSO in fryReceiptSOArray)
        {
            if (fryReceiptSO.input == inputKitchenObjectSo)
            {
                return fryReceiptSO;
            }
        }
        return null;
    }
}
