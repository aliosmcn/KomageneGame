using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderSystem : MonoTimer
{
    [Header("Events")]
    [SerializeField] private VoidEvent onGameStarted;
    [SerializeField] private OrderSOEvent onOrderTimeOut;
    [SerializeField] private OrderSOEvent onOrderCreated;
    [SerializeField] private ItemSOEvent onOrderDelivered; //bu teslim tezgahında raise edilecek
    [SerializeField] private OrderSOEvent onOrderClosed;
    [SerializeField] private IntEvent onMoneyValueChanged;

    [Header("Particles")]
    [SerializeField] private ParticleSystem orderCorrectParticle;
    [SerializeField] private ParticleSystem orderFalseParticle;

    // Gelebilecek butun orderlar
    [SerializeField] private List<OrderSO> Orders;
    //Order gelme sureleri ve sayısal degerler
    [SerializeField] private float orderTime = 3f;
    [SerializeField] private int maxOrderCount = 3;
    int randomOrderIndex;

    // Gelmiş orderlar
    [SerializeField] private List<OrderSO> currentOrders = new List<OrderSO>();

    public int money;
    

    private void OnEnable()
    {
        onOrderTimeOut.AddListener(CloseOrder);
        onOrderDelivered.AddListener(CheckOrderCorrect);
        //onGameStarted.AddListener(StartGame);
    }

    private void OnDisable()
    {
        onOrderTimeOut.RemoveListener(CloseOrder);
        onOrderDelivered.RemoveListener(CheckOrderCorrect);
        //onGameStarted.RemoveListener(StartGame);
    }

    private void Start()
    {

        base.SetRemainingTime(orderTime);
        base.StartTimer();
    }

    protected override void TimeIsUp()
    {
        CreateNewOrder();
        ResetTimer();
    }

    private void CreateNewOrder()
    {
        if (currentOrders.Count >= maxOrderCount) return;
        randomOrderIndex = Random.Range(0, Orders.Count);
        OrderSO newOrder = Orders[randomOrderIndex];
        currentOrders.Add(newOrder);
        onOrderCreated.Raise(newOrder);
    }

    public void CloseOrder(OrderSO toCloseOrder)
    {
        currentOrders.Remove(toCloseOrder);
        onMoneyValueChanged.Raise(-20);
    }

    private void CloseOrder(OrderSO toCloseOrder, bool waiterClose)
    {
        currentOrders.Remove(toCloseOrder);
        onOrderClosed.Raise(toCloseOrder);
    }

    private void CheckOrderCorrect(ItemSO deliveredMeal)
    {
        foreach (OrderSO order in currentOrders)
        {
            if (order.OrderRecipe.result == deliveredMeal)
            {
                CloseOrder(order, true);
                TrueOrder();
                return;
            }
        }
        FalseOrder();
    }

    private void TrueOrder()
    {
        onMoneyValueChanged.Raise(100);
        AudioManager.Instance.PlaySFX("trueOrder");
        orderCorrectParticle.Play();
    }
    private void FalseOrder()
    {
        onMoneyValueChanged.Raise(-20);
        AudioManager.Instance.PlaySFX("falseOrder");
        orderFalseParticle.Play();
    }
    

}
