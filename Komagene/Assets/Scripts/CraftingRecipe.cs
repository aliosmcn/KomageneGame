using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


[CreateAssetMenu(menuName = "Crafting/Recipe", fileName = "NewRecipe")]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemSO> materials;
    public ItemSO result;

    [SerializeField] public bool sliceableRecipe = false;
}
