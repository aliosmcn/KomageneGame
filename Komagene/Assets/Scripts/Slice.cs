using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slice : MonoBehaviour
{
    [SerializeField] List<ItemSO> slicedObjects;
    [SerializeField] private VoidEvent onSliceToggle;


    [SerializeField] private bool playerHere;
    [SerializeField] private GameObject dustParticle;

    bool canSlice = true;

    void Update()
    {
        SliceObject();
    }

    void SliceObject()
    {
        if (playerHere && Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (this.transform.childCount > 1 && transform.GetChild(1).GetComponent<Item>().ItemData.CanSlice)
            {
                if (canSlice)
                {
                    canSlice = false;
                    onSliceToggle.Raise();
                    dustParticle.SetActive(true);
                    AudioManager.Instance.PlaySFX("Dograma");
                    Invoke(nameof(Destroy), 3f);
                }            }
        }
    }

    void Destroy()
    {
        onSliceToggle.Raise();
        dustParticle.SetActive(false);
        string newObjectID = transform.GetChild(1).GetComponent<Item>().ItemData.ItemID + "D";
        CreateNewObject(newObjectID);
        Destroy(transform.GetChild(1).gameObject);
        canSlice = true;
    }

    void CreateNewObject(string objID)
    {
        
        foreach (var item in slicedObjects)
        {
            
            if (item.ItemID == objID)
            {
                GameObject slicedObject = Instantiate(item.prefab);
                slicedObject.transform.parent = this.transform;
                slicedObject.transform.position = new Vector3(transform.position.x, transform.position.y + 0.38f, transform.position.z);

                return;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHere = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        playerHere = false;
    }

}
