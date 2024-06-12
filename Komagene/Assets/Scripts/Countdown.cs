using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;

public class Countdown : MonoTimer
{
    [Header("Events")]
    [SerializeField] private VoidEvent onTimeFinished;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Image timerImg;
    [SerializeField] private int currentTime;
    [SerializeField] private int duration;
    


    string hile = "";

    void Start()
    {
        base.SetRemainingTime(1f);
        base.StartTimer();
    }

    protected override void Update()
    {
        base.Update();
        if (currentTime < duration)
        {
            timerImg.fillAmount = Mathf.InverseLerp(0, duration, currentTime);
            timerText.text = (currentTime / 60).ToString("00") + ":" + (currentTime % 60).ToString("00");
        }
        if (currentTime <= 0)
        {
            onTimeFinished.Raise();
            base.StopTimer();
            currentTime = 50;
            base.PauseTimer();
        }
        Hile();
    }
    protected override void TimeIsUp()
    {
        currentTime -= 1;
        base.RestartTimer();
    }
    private void Hile()
    {
        if (Input.anyKeyDown)
        {
            hile += Input.inputString;


            if (hile == "ali")
            {
                currentTime = 1;


                hile = "";
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                hile = "";
            }
        }
    }
}
