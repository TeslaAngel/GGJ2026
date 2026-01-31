using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for Legacy UI Text
using TMPro; // Use this if you are using Text Mesh Pro

public class MonsterCooler : MonoBehaviour
{
    public float timeRemaining = 10f; // The initial countdown time in seconds
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText; // Reference to the Text Mesh Pro UI element
    //In the Unity Editor, drag the UI Text object from the Hierarchy window to the "Time Text" field in the CountdownTimer script component in the Inspector window.

    // Start is called before the first frame update
    void Start()
    {
        // Start the timer automatically when the game starts
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                // Add game over or other end-of-countdown logic here
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        // Ensure the time is not negative when displaying
        timeToDisplay += 1;

        // Calculate minutes and seconds
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Format the time string to ensure leading zeros (e.g., 05:00 instead of 5:0)
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
