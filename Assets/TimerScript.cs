using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public float startTime = 30f;
    private float currentTime;
    private bool isRunning = true;

    public TextMeshProUGUI timerText;

    void Start()
    {
        currentTime = startTime;
        UpdateUI();
    }

    void Update()
    {
        if (!isRunning) return;

        currentTime -= Time.deltaTime;
        currentTime = Mathf.Clamp(currentTime, 0f, startTime);

        UpdateUI();

        if (currentTime <= 0f)
        {
            TimeUp();
        }
    }

    void UpdateUI()
    {
        timerText.text = Mathf.Ceil(currentTime).ToString();
    }

    void TimeUp()
    {
        isRunning = false;
        Debug.Log("Time's up!");
        // FAIL LOGIC HERE
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
