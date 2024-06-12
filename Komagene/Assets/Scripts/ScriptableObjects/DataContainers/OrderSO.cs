using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OrderSystem/Order", fileName = "NewOrder")]
public class OrderSO : ScriptableObject
{
    [SerializeField] private Sprite orderSprite;
    [SerializeField] private CraftingRecipe orderRecipe;

    public CraftingRecipe OrderRecipe
    {
        get
        {
            return orderRecipe;
        }
    }
}
