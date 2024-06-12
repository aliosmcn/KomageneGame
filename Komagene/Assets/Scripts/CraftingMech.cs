using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMech : MonoBehaviour
{

    #region Singleton
    private static CraftingMech instance;

    public static CraftingMech Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    #endregion Singleton

    [SerializeField] private List<CraftingRecipe> recipes;


    // Yaratılacak objeyi döndürür.
    public ItemSO SearchRecipes(string item1id, string item2id)
    {
        foreach(var recipe in recipes)
        {
            if (recipe.sliceableRecipe)
            {
                continue;
            }

            int count = 0;

            foreach (var material in recipe.materials)
            {

                if(item1id.Equals(material.ItemID))
                {
                    count++;
                }

                if (item2id.Equals(material.ItemID))
                {
                    count++;
                }
                if (count == 2)
                {
                    return recipe.result;
                }
            }
        }
        return null;
    }
}
