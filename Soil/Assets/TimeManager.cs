using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private Text clockDisplay;
    private Text dayCounter;
    private int currentDay = 0;

    int currentTime = 720;
    int midnight = 1440;

    private void Start()
    {
        clockDisplay = GameObject.Find("Canvas/Clock and Day/Time").GetComponent<Text>();
        dayCounter = GameObject.Find("Canvas/Clock and Day/Day").GetComponent<Text>();

        InitializeTimer();
    }

    private void InitializeTimer()
    {
        RefreshDayCounter();
        RefreshTimerUI();
        StartCoroutine(Time());
    }

    private void RefreshTimerUI()
    {
        string minutes = (currentTime / 60).ToString();
        string seconds = "00";//(currentTime % 60).ToString("00");
        clockDisplay.text = $"{minutes}:{seconds}";
    }

    private void RefreshDayCounter()
    {
        dayCounter.text = $"Day {currentDay}";
    }

    IEnumerator Time()
    {
        yield return new WaitForSeconds(0.5f);

        currentTime += 1;

        if (currentTime >= midnight)
        {
            currentTime = 0;
            currentDay++;
            RefreshDayCounter();
        }

        RefreshTimerUI();
        StartCoroutine(Time());
    }
}
