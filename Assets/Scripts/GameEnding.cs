using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameEnding : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;

    private int keyCount;

    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
        DisplayTime(timeRemaining);

        keyCount = 3;
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
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = "Time Remaining: " + string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "Key":
                other.gameObject.SetActive(false);
                keyCount = keyCount - 1;

            case "Door1":
                if (keyCount = 2)
                {
                    other.gameObject.SetActive(false);
                }

            case "Door2":
                if (keyCount = 1)
                {
                    other.gameObject.SetActive(false);
                }

            case "Door3":
                if (keyCount = 0)
                {
                    other.gameObject.SetActive(false);
                }
        }
    }
}
