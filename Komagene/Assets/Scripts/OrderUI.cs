using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OrderUI : MonoTimer
{
    [Header("Events")]
    [SerializeField] private VoidEvent onGameStarted;
    [SerializeField] private OrderSOEvent onOrderCreated;
    [SerializeField] private OrderSOEvent onOrderClosed;
    [SerializeField] private OrderSOEvent onOrderTimeOut;
    
    
    [SerializeField] private float orderTime = 25f; 
    [SerializeField] public OrderSO currentOrder;

    [SerializeField] private Image fillImage;

    [SerializeField] private float elapsedTime = 0f;

    private void OnEnable()
    {
        //onGameStarted.AddListener(StartGame);
    }
    private void OnDisable()
    {
        //onGameStarted.RemoveListener(StartGame);
    }
    void Start()
    {
        base.SetRemainingTime(0.01f);
        base.StartTimer();
    }

    protected override void Update()
    {
        base.Update();
        if (elapsedTime < orderTime)
        {
            fillImage.fillAmount = 1f - (elapsedTime / orderTime);
        }
        else
        {
            onOrderTimeOut.Raise(currentOrder);
            onOrderClosed.Raise(currentOrder);
        }
    }

    protected override void TimeIsUp()
    {
        elapsedTime += 0.01f;
        base.RestartTimer();
    }


}
