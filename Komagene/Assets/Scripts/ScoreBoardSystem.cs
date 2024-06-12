using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreBoardSystem : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private VoidEvent onStartAnimation;
    [SerializeField] private VoidEvent onEndAnimation;

    [SerializeField] private InputField inputField;
    [SerializeField] private GameObject scoreBoardPanel;
    [SerializeField] private Text placeHolderText;

    [SerializeField] private GameObject inputPanel;

    public string playerName;

    private void Update()
    {
        if (inputPanel.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                InputFieldAndStartGame();
            }
        }
    }
    public void ScoreBoardButton(bool state)
    {
        scoreBoardPanel.SetActive(state);
    }

    public void InputFieldAndStartGame()
    {
        
        if (inputField.text!="")
        {
            playerName = inputField.text;

            StartAnim();
        }
        else
        {
            placeHolderText.text = "Ýsim boþ býrakýlamaz.";
            placeHolderText.color = Color.red;
        }
    }
    private void StartAnim()
    {
        onEndAnimation.Raise();
        Invoke(nameof(StartLevel), 0.8f);
    }

    private void StartLevel()
    {
        SceneManager.LoadScene(1);
    }
}
