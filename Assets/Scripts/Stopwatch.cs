using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    bool stopwatchActive = false;
    float currentTime;
    public TextMeshProUGUI currentTimeText;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        PlayerPrefs.SetFloat("count", 0);
        StartStopwatch();
    }

    // Update is called once per frame
    void Update()
    {
        if (stopwatchActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
            PlayerPrefs.SetFloat("count", PlayerPrefs.GetFloat("count") + Time.deltaTime);
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        PlayerPrefs.SetString("Time", time.ToString(@"mm\:ss"));
        currentTimeText.text = time.ToString(@"mm\:ss");
    }

    public void StartStopwatch()
    {
        stopwatchActive = true;
    }

    public void StopStopwatch()
    {
        stopwatchActive = false;
    }
}
