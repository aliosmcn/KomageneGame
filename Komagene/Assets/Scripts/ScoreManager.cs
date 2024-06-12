using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private Text scoreNameText;
    [SerializeField] private Text scoreText;

    string AD = "";
    string SKOR = "";

    public int score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SetPrefs(string isim, int gameScore)
    {
        if (int.TryParse(scoreText.text, out score))
        {
            if (gameScore > score)
            {
                scoreNameText.text = isim;
                scoreText.text = gameScore.ToString();
                PlayerPrefs.SetString(AD, isim);
                PlayerPrefs.SetInt(SKOR, gameScore);
            }
        }
    }

    public void GetPrefs()
    {
        if (PlayerPrefs.GetString(AD) != null && PlayerPrefs.GetInt(SKOR).ToString() != null)
        {
            scoreNameText.text = PlayerPrefs.GetString(AD);
            scoreText.text = PlayerPrefs.GetInt(SKOR).ToString();
        }
    }
}
