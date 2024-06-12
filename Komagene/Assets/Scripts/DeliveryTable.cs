using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DeliveryTable : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private ItemSOEvent onOrderDelivered;
    [SerializeField] private VoidEvent onSpacePressed;

    private Animator animator;

    GameObject delivery;

    private void OnEnable()
    {
        onOrderDelivered.AddListener(PlayAnimation);
    }
    private void OnDisable()
    {
        onOrderDelivered.RemoveListener(PlayAnimation);
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void PlayAnimation(ItemSO empty)
    {
        animator.SetBool("delivery", true);
        
        Invoke(nameof(RemoveAnim), 1f);
    }
    private void RemoveAnim()
    {
        animator.SetBool("delivery", false);
    }
    public void Delivery(GameObject deliveryObject)
    {
        if (deliveryObject)
        {
            if (deliveryObject.GetComponent<Combiner>() && deliveryObject.GetComponent<Item>().ItemData.ItemID != "T")
            {
                onOrderDelivered.Raise(deliveryObject.GetComponent<Item>().ItemData);
                delivery = deliveryObject;
                GetComponent<Tezgah>().ContainedObject = null;
                Invoke(nameof(DestroyDelivery), 0.5f);
            }
        }
    }

    private void DestroyDelivery()
    {
        Destroy(delivery);
    }
}
