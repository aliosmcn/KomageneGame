using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private VoidEvent onGameStarted;
    [SerializeField] private IntEvent onMoneyValueChanged;
    [SerializeField] private ItemSOEvent onItemPicked;
    [SerializeField] private VoidEvent onTimeFinished;
    [SerializeField] private ItemSOEvent onOrderDelivered;
    [SerializeField] private VoidEvent onStartAnimation;
    [SerializeField] private VoidEvent onEndAnimation;

    [Header("UI")]
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private Text itemsText;
    [SerializeField] private Text hasilatText;
    [SerializeField] private Text ordersText;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject recipePanel;

    //List<GameObject> foods = new List<GameObject>();
    List<string> deliveredOrders = new List<string>();
    List<string> pickedItems = new List<string>();
    public int money;

    Dictionary<string, int> itemCounts = new Dictionary<string, int>();
    Dictionary<string, int> orderCounts = new Dictionary<string, int>();

    private void OnEnable()
    {
        onMoneyValueChanged.AddListener(MoneyValueChange);
        onItemPicked.AddListener(AddItemList);
        onTimeFinished.AddListener(TimeFinished);
        onOrderDelivered.AddListener(AddOrderList);
    }
    private void OnDisable()
    {
        onMoneyValueChanged.RemoveListener(MoneyValueChange);
        onItemPicked.RemoveListener(AddItemList);
        onTimeFinished.RemoveListener(TimeFinished);
        onOrderDelivered.RemoveListener(AddOrderList);
    }
    private void Start()
    {
        onStartAnimation.Raise();
    }
    
    private void MoneyValueChange(int deger)
    {
        money += deger;
    }

    private void AddItemList(ItemSO item)
    {
        pickedItems.Add(item.itemName);
    }

    private void AddOrderList(ItemSO order)
    {
        deliveredOrders.Add(order.itemName);
    }


    private void TimeFinished()
    {
        finishPanel.SetActive(true);
        AudioManager.Instance.PlaySFX("timeOver");
        AudioManager.Instance.musicSource.pitch = 1f;
        if (money >= 0) hasilatText.color = Color.green;
        else hasilatText.color = Color.red;
        #region WriteUITexts
        hasilatText.text = money.ToString();
        //TESLÝM EDÝLEN SÝPARÝÞLERÝ YAZDIR
        foreach (string item in deliveredOrders)
        {
            if (orderCounts.ContainsKey(item))
            {
                orderCounts[item]++;
            }
            else
            {
                orderCounts[item] = 1;
            }
        }
        foreach (KeyValuePair<string, int> kvp in orderCounts)
        {
            ordersText.text += $"{kvp.Key} x {kvp.Value}" + "\n";
        }
        //KULLANILAN ITEMLARI YAZDIR
        foreach (string item in pickedItems)
        {
            if (itemCounts.ContainsKey(item))
            {
                itemCounts[item]++;
            }
            else
            {
                itemCounts[item] = 1;
            }
        }
        foreach (KeyValuePair<string, int> kvp in itemCounts)
        {
            itemsText.text += $"{kvp.Key} x {kvp.Value}" + "\n";
        }
        #endregion UITexts
    }


    private void Update()
    {
        if (finishPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                onEndAnimation.Raise();
                Invoke(nameof(StartMenu), 0.8f);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    onEndAnimation.Raise();
                    Invoke(nameof(NextLevel), 0.8f);
                }
            }
        }
        if (recipePanel.activeSelf)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                recipePanel.GetComponent<Animator>().Play("RecipeAnim");
                Invoke(nameof(PlaySound), 0.25f);
                Invoke(nameof(EndRecipeAnim), 1f);
                
            }
        }
    }

    
    private void StartMenu()
    {
        finishPanel.SetActive(false);
        hasilatText.text = "";
        itemsText.text = "";
        ordersText.text = "";
        SceneManager.LoadScene("Menu");
    }
    private void NextLevel()
    {
        finishPanel.SetActive(false);
        hasilatText.text = "";
        itemsText.text = "";
        ordersText.text = "";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    private void PlaySound()
    {
        AudioManager.Instance.PlaySFX("Anim");
    }
    private void EndRecipeAnim()
    {
        onGameStarted.Raise();
        recipePanel.SetActive(false);
        inGameUI.SetActive(true);
        
    }

}
