using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering.UI;

public class PickAndDrop : MonoBehaviour
{
    [SerializeField] private LayerMask interactMask;

    bool isChildOfTable;

    private Rigidbody rb;
    private BoxCollider bC;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bC = GetComponent<BoxCollider>();
    }
    
    private void Update()
    {
        if (this.transform.parent == null && !isChildOfTable)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            isChildOfTable = true;
        }
            
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Masa") || collision.gameObject.CompareTag("DogramaTahtasi"))
        {
            Vector3 normal = collision.contacts[0].normal;

            if (normal == Vector3.up)
            {
                if (collision.gameObject.transform.childCount == 0)
                {
                    this.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y + 0.40f, collision.transform.position.z);
                    this.transform.parent = collision.transform;
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    isChildOfTable = false;
                    bC.enabled = true;
                }
            }
        }
    }*/
}
