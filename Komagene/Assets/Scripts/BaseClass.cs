using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public abstract class BaseClass : MonoBehaviour
{
    public abstract void MyAbstractFunction();
}

public class SubClass : BaseClass
{
    public float sayac = 10f;
    private float saniye = 0f;
    public TextMeshProUGUI sayacText;

    public bool sureBitti;

    bool canCount;
    public override void MyAbstractFunction()
    {
        if (canCount)
        {
            saniye += Time.deltaTime;

            if (saniye >= 1)
            {
                sayac--;
                saniye = 0f;
                sayacText.text = sayac.ToString();
                if (sayac == 0)
                {
                    TimeOut();
                    Debug.Log("süre bitti");
                }
            }
        }
    }
    void TimeOut()
    {
        canCount = false;
    }
}
public class DersSc : MonoBehaviour
{
    BaseClass sub;
    
    private void Update()
    {
        sub.MyAbstractFunction();
    }

}

