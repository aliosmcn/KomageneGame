using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private VoidEvent onStartAnimation;
    [SerializeField] private GameObject inputPanel;
    [SerializeField] private GameObject highScorePanel;

    private void Update()
    {
        if (inputPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                inputPanel.GetComponent<Animator>().Play("AdGidis");
                Invoke("PlaySound", 0.25f);
                Invoke("InputActive", 0.8f);
            }
        }
        if (highScorePanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                highScorePanel.SetActive(false);
            }
        }
    }
    
    private void PlaySound()
    {
        AudioManager.Instance.PlaySFX("Anim");
    }
    private void InputActive()
    {
        inputPanel.SetActive(false);
    }

    public void HighScoreButton()
    {
        AudioManager.Instance.PlaySFX("Click");
        highScorePanel.SetActive(true);
    }
    public void BaslaButton()
    { 
        inputPanel.SetActive(true);
        AudioManager.Instance.PlaySFX("Anim");
    }
    public void CikisButton()
    {
        Debug.Log("Oyundan çýkýldý.");
        AudioManager.Instance.PlaySFX("Click");
        Application.Quit();
    }
}
