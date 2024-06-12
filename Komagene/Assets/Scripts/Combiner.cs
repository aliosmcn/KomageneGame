using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Combiner : MonoBehaviour
{
    [SerializeField] private ItemSO currentPlateObject;
    GameObject temas;
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("item")) return;
        ItemSO itemso = collision.gameObject.GetComponent<Item>().ItemData;
        if (itemso.ItemID.StartsWith("T"))
        {
            return;
        }
        temas = collision.gameObject;
        SearchRecipe(itemso.ItemID);
    }
    
    public bool SearchRecipe2(string itemid, Vector3 spawnPos)
    {
        ItemSO toCraftitemSO = CraftingMech.Instance.SearchRecipes(itemid, currentPlateObject.ItemID);
        if (toCraftitemSO == null) return false;
        CreateNewItemFromRecipe(toCraftitemSO, spawnPos);
        return true;
    }

    private void CreateNewItemFromRecipe(ItemSO toCreateObject, Vector3 spawnPos)
    {
        GameObject toInstantiate = Instantiate(toCreateObject.prefab);
        if (ClosestObjectManager.Instance.nearestObject.GetComponent<Tezgah>().ContainedObject != null)
        {
            Destroy(ClosestObjectManager.Instance.nearestObject.GetComponent<Tezgah>().ContainedObject);
        }
        ClosestObjectManager.Instance.nearestObject.GetComponent<Tezgah>().ContainedObject = toInstantiate;
        toInstantiate.transform.position = spawnPos;
        Destroy(gameObject);
    }

    private void SearchRecipe(string itemid)
    {
        ItemSO toCraftitemSO = CraftingMech.Instance.SearchRecipes(itemid, currentPlateObject.ItemID);
        if (toCraftitemSO == null) return;
        GameObject toInstantiate = Instantiate(toCraftitemSO.prefab);
        toInstantiate.transform.position = transform.position;
        Destroy(temas);
        Destroy(this.gameObject);
    }

    
}
