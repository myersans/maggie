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

    public TextMeshProUGUI clue1Text;
    public TextMeshProUGUI clue2Text;
    public TextMeshProUGUI clue3Text;
    public TextMeshProUGUI warningText;
    public TextMeshProUGUI keyFoundText;

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

    void SetClue1Text()
    {
        clue1Text.text = "Free Bird/nOver Yonder/nRejoice in your Flight/nThe wind, your palace";
    }

    void SetClue2Text()
    {
        clue2Text.text = "Calm waves/nRoaring sea/nAbrupt End/nCastaway/nKissed by the scorching sun";
    }

    void SetClue3Text()
    {
        clue3Text.text "Whispers beckon/nTis' too late I reckon/nNo return/nDo you feel the burn/nOpen the gates of hell/nWelcome, things are just swell";
    }

    void SetWarningText()
    {
        warningText.text = "You need the key if you wish to flee!";
    }

    void SetKeyFoundText()
    {
        keyFoundText.text = "Key Found! Hurry to the next room!";
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "Key":
                other.gameObject.SetActive(false);
                keyCount = keyCount - 1;
                SetKeyFoundText();

            case "Door1":
                if (keyCount = 2)
                {
                    other.gameObject.SetActive(false);
                }
                else
                {
                    SetWarningText();
                }

            case "Door2":
                if (keyCount = 1)
                {
                    other.gameObject.SetActive(false);
                }
                else
                {
                    SetWarningText();
                }

            case "Door3":
                if (keyCount = 0)
                {
                    other.gameObject.SetActive(false);
                }
                else
                {
                    SetWarningText();
                }

            case "Clue1":
                SetClue1Text();

            case "Clue2":
                SetClue2Text();

            case "Clue3":
                SetClue3Text();
        }
    }
}
