using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{

    [SerializeField] private string itemId;

    public string ItemID
    {
        get
        {
            return itemId;
        }
    }

    [SerializeField] public string itemName;
    [SerializeField] private Sprite itemIcon;

    public Sprite ItemIcon
    {
        get
        {
            return itemIcon;
        }
    }

    [SerializeField] public GameObject prefab;
    [SerializeField] private bool canSlice;

    public bool CanSlice
    {
        get
        {
            return canSlice;
        }
    }


}
