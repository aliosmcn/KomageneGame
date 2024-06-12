using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPickController : Character
{

    [Header("Events")]
    [SerializeField] private VoidEvent onGameStarted;
    [SerializeField] private VoidEvent onSpacePressed;
    [SerializeField] private ItemSOEvent onOrderDelivered;

    [Header("Objects")]
    [SerializeField] public GameObject containingObject = null; // Karakterin tuttuğu

    ClosestObjectManager closestObjectManager;

    [SerializeField] private GameObject toPickObject = null;
    Rigidbody rigid;

    [SerializeField] private Transform HoldPoint;

    Tezgah tezgah;

    protected override void OnEnable()
    {
        base.OnEnable();
        onSpacePressed.AddListener(SpacePressed);
        onOrderDelivered.AddListener(ClearToPickObject);
        //onGameStarted.AddListener(StartGame);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        onSpacePressed.RemoveListener(SpacePressed);
        onOrderDelivered.RemoveListener(ClearToPickObject);
        //onGameStarted.RemoveListener(StartGame);
    }

    protected override void Start()
    {
        base.Start();
        closestObjectManager = GetComponent<ClosestObjectManager>();
    }

    private void ClearToPickObject(ItemSO example)
    {
        toPickObject = null;
    }

    private void SpacePressed()
    {
        // ----------- ELİMİZDE OBJE YOKSA ------------
        if(containingObject == null)
        {
            containingObject = toPickObject;
            if (toPickObject != null) rigid = containingObject.GetComponent<Rigidbody>();
            else return;
            AudioManager.Instance.PlaySFX("pick");
            containingObject.transform.position = HoldPoint.position;
            containingObject.transform.SetParent(HoldPoint);
            SetRbAndColliderActive(false);
            if (closestObjectManager.nearestObject != null)
            {
                tezgah = closestObjectManager.nearestObject.GetComponent<Tezgah>();
                if (toPickObject == tezgah.ContainedObject)
                {
                    tezgah.ContainedObject = null;
                    //MAŞALLAH
                }
                
                
                
            }
        }
        else // --------- ELİM DOLU, İTEMİ BIRAK ----------
        {
            //YERE BIRAK
            if (closestObjectManager.nearestObject == null)
            {
                AudioManager.Instance.PlaySFX("drop");
                SetRbAndColliderActive(true);
                containingObject.transform.parent = null;
                containingObject = null;
            }
            else //MASAYA BIRAK
            {
                tezgah = closestObjectManager.nearestObject.GetComponent<Tezgah>();
                Vector3 spawnPos = new Vector3(closestObjectManager.nearestObject.transform.position.x, closestObjectManager.nearestObject.transform.position.y + 0.40f, closestObjectManager.nearestObject.transform.position.z);
                if (tezgah.ContainedObject != null)
                {
                    //*  tezgahtaki obje combiner sınıfını ve bırakmak istediğimiz obje combiner sınıfını taşıyor mu ?
                    if (tezgah.ContainedObject.GetComponent<Combiner>() && containingObject.GetComponent<Combiner>())
                    {
                        return;
                    }
                    //tezgahtaki obje ve bırakmak istediğimiz objelerin ikisinde de combiner yok mu ?
                    if (!tezgah.ContainedObject.GetComponent<Combiner>() && !containingObject.GetComponent<Combiner>())
                    {
                        return;
                    }
                    //tezgahtaki tabak
                    if (tezgah.ContainedObject.GetComponent<Combiner>())
                    {
                        Combiner c = tezgah.ContainedObject.GetComponent<Combiner>();
                        if (c.SearchRecipe2(containingObject.GetComponent<Item>().ItemData.ItemID, spawnPos))
                        {
                            Destroy(containingObject);
                            AudioManager.Instance.PlaySFX("drop");
                        }
                        return;
                    }
                    //elimdeki tabak
                    if (containingObject.GetComponent<Combiner>())
                    {
                        Combiner c = containingObject.GetComponent<Combiner>();
                        c.SearchRecipe2(tezgah.ContainedObject.GetComponent<Item>().ItemData.ItemID, spawnPos);
                        AudioManager.Instance.PlaySFX("drop");
                        return;
                    }

                }
                else
                {
                    //TEZGAH BOŞSA
                    if (tezgah.gameObject.tag == "Teslim" && !containingObject.GetComponent<Combiner>()) return;
                    if (tezgah.gameObject.tag == "Teslim" && containingObject.GetComponent<Item>().ItemData.ItemID == "T") return;
                    if (tezgah.gameObject.tag == "DogramaTahtasi" && !containingObject.GetComponent<Item>().ItemData.CanSlice) return;
                    tezgah.SetContainedObject(containingObject);
                    AudioManager.Instance.PlaySFX("drop");
                    SetRbAndColliderActive(false);
                    containingObject.transform.parent = closestObjectManager.nearestObject.transform;
                    containingObject.transform.position = new Vector3(closestObjectManager.nearestObject.transform.position.x, closestObjectManager.nearestObject.transform.position.y + 0.42f, closestObjectManager.nearestObject.transform.position.z);
                    containingObject = null;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("item"))
        {
            toPickObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == toPickObject)
        {
            toPickObject = null;
        }
    }
    private void SetRbAndColliderActive(bool isActive)
    {
        rigid.useGravity = isActive;
        rigid.isKinematic = !isActive; 
        containingObject.GetComponent<BoxCollider>().isTrigger = !isActive;
    }

    
}
