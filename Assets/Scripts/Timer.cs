using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text timer;

    float time = 0f;

    public void Update()
    {
        time += Time.deltaTime;
        int timeInt = Convert.ToInt32(Math.Round(time));
        if (timeInt % 60 < 10)
        {
            timer.text = $"{timeInt / 60}:0{timeInt % 60}";
        }
        else
        {
            timer.text = $"{timeInt / 60}:{timeInt % 60}";
        }
    }

    public int getTime()
    {
        return Convert.ToInt32(Math.Round(time));
    }

}
