using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static Unity.Burst.Intrinsics.Arm;

public class Till : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private VoidEvent onGameStarted;
    [SerializeField] private IntEvent onMoneyValueChanged;
    [SerializeField] private ItemSOEvent onItemPicked;

    [Header("Prefabs")]
    [SerializeField] private GameObject domates;
    [SerializeField] private GameObject tabak;
    [SerializeField] private GameObject marul;
    [SerializeField] private GameObject cigKofte;
    [SerializeField] private GameObject limon;
    [SerializeField] private GameObject narEksisi;

    Rigidbody objRb;
    BoxCollider objCll;

    Animator animator;
    private bool playerInside = false;
    bool canSendItemSO = false;

    private void OnEnable()
    {
        onGameStarted.AddListener(StartGame);
    }
    private void OnDisable()
    {
        onGameStarted.RemoveListener(StartGame);
    }

    void StartGame()
    {
        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
        }
        onMoneyValueChanged.Raise(4);/*
        switch (this.tag)
        { 
            case "DomatesSpawn":
                Spawn(domates);
                break;
            case "TabakSpawn":
                SpawnTabak();
                break;
            case "MarulSpawn":
                Spawn(marul);
                break;
            case "CigKofteSpawn":
                Spawn(cigKofte);
                break;
            case "LimonSpawn":
                Spawn(limon);
                break;
            default:
                break;
        }*/
        canSendItemSO = true;
    }

    private void Update()
    {
        if (this.transform.childCount < 2)
        {
            switch (this.tag)
            {
                case "DomatesSpawn":
                    Spawn(domates);
                    break;
                case "TabakSpawn":
                    SpawnTabak();
                    break;
                case "MarulSpawn":
                    Spawn(marul);
                    break;
                case "CigKofteSpawn":
                    Spawn(cigKofte);
                    break;
                case "LimonSpawn":
                    Spawn(limon);
                    break;
                default:
                    break;
            }
        }
        if (playerInside && Input.GetKeyDown(KeyCode.Space) && GetComponent<Animator>())
        {
            animator.Play("KasaAnim");
        }
    }

    void Spawn(GameObject nesne)
    {
        if (GameObject.Find("Hold").transform.childCount == 0)
        {
            GameObject newObject;
            newObject = Instantiate(nesne);
            newObject.transform.position = new Vector3(this.transform.position.x, transform.position.y - 0.1f, transform.position.z);
            newObject.transform.localScale = nesne.transform.localScale;
            newObject.transform.SetParent(transform);
            objCll = GetComponentInParent<BoxCollider>();
            objRb = GetComponentInParent<Rigidbody>();
            onMoneyValueChanged.Raise(-5);
            if (canSendItemSO) onItemPicked.Raise(newObject.GetComponent<Item>().ItemData);
        }
    }

    void SpawnTabak()
    {
        if (GameObject.Find("Hold").transform.childCount == 0)
        {
            GameObject newObject;
            newObject = Instantiate(tabak);
            newObject.transform.position = new Vector3(this.transform.position.x, transform.position.y + 0.5f, this.transform.position.z);
            newObject.transform.localScale = tabak.transform.localScale;
            newObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInside = true;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false; // Karakter kutudan çýktý
        }
    }
}
