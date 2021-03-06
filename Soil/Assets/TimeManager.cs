using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private Text clockDisplay;
    private Text dayCounter;
    private int currentDay = 0;

    int currentTime = 0;
    int midnight = 1440;

    public Material skyMat;

    private void Update()
    {
        //skyMat.SetFloat("Vector1_7ED56D81", Mathf.Abs(Mathf.Sin(((currentTime / 60.0f) / (midnight / 60.0f)) * Mathf.PI)));
    }

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
        string seconds = (currentTime % 60).ToString("00");
        clockDisplay.text = $"{minutes}:{seconds}";
    }

    private void RefreshDayCounter()
    {
        dayCounter.text = $"Day {currentDay}";
    }

    IEnumerator Time()
    {
        yield return new WaitForSeconds(0.05f);

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
