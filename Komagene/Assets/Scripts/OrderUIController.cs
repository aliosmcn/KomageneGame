using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class OrderUIController : MonoBehaviour
{

    [SerializeField] private OrderSOEvent onOrderCreated;
    [SerializeField] private OrderSOEvent onOrderClosed;
    
    [SerializeField] private GameObject orderUIPrefab;

    private List<GameObject> orderUIs = new List<GameObject>();
    

    private void OnEnable()
    {
        onOrderCreated.AddListener(CreateOrderUI);
        onOrderClosed.AddListener(RemoveOrderFromUI);
    }

    private void OnDisable()
    {
        onOrderCreated.RemoveListener(CreateOrderUI);
        onOrderClosed.RemoveListener(RemoveOrderFromUI);
    }
    
    private void CreateOrderUI(OrderSO order)
    {
        Sprite recipeIcon = order.OrderRecipe.result.ItemIcon;
        orderUIPrefab.GetComponent<Image>().sprite = recipeIcon;
        orderUIPrefab.GetComponent<OrderUI>().currentOrder = order;
        GameObject temp = Instantiate(orderUIPrefab, this.transform);
        temp.name = Random.Range(1, 100).ToString();
        orderUIs.Add(temp);
    }
    
    private void RemoveOrderFromUI(OrderSO orderToClose)
    {
        //ReverseList();
        
        int removeInt2 = -1;
        foreach (GameObject orderUi in orderUIs)
        {
            OrderSO tempOseo = orderUi.GetComponent<OrderUI>().currentOrder;
            if (tempOseo.GetHashCode() == orderToClose.GetHashCode())
            {
                removeInt2 = orderUIs.IndexOf(orderUi);
                break;
            }
        }
        GameObject tempObject = orderUIs[removeInt2];
        Destroy(tempObject);
        orderUIs.RemoveAt(removeInt2);

        
    }
}
