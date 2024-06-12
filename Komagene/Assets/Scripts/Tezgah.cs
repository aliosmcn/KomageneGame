using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Tezgah : MonoBehaviour
{
    // Masada duran obje
    [SerializeField] private GameObject containedObject = null;
    [SerializeField] private Material focusedMaterial, unFocusedMaterial;
    
    public GameObject ContainedObject
    {
        get
        {
            return this.containedObject;
        }
        set
        {
            containedObject = value;
        }
    }
    
    public void SetContainedObject(GameObject toContainObject)
    {
        this.containedObject = toContainObject;
        Item item = toContainObject.GetComponent<Item>();
        if (GetComponent<DeliveryTable>())
        {
            GetComponent<DeliveryTable>().Delivery(toContainObject);
        }
    }

}
