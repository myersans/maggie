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
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    bool m_HasAudioPlayed;
    float m_Timer;

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
            else
            {
                if (keyCount == 0)
                {
                    EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
                }
                else
                {
                    EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
                }
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

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                Application.Quit();
            }
        }
    }

    void SetClue1Text()
    {
        clue1Text.text = "Free Bird Over Yonder Rejoice in your flight The wind, your palace";
    }

    void SetClue2Text()
    {
        clue2Text.text = "Calm waves Roaring sea Abrupt End Castaway Kissed by the scorching sun";
    }

    void SetClue3Text()
    {
        clue3Text.text = "Whispers beckon Tis' too late I reckon No return Do you feel the burn Open the gates of hell Welcome, things are just swell";
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
                keyFoundText.gameObject.SetActive(true);
                SetKeyFoundText();
                break;

            case "Door1":
                if (keyCount == 2)
                {
                    other.gameObject.SetActive(false);
                    break;
                }
                else
                {
                    warningText.gameObject.SetActive(true);
                    SetWarningText();
                    break;
                }

            case "Door2":
                if (keyCount == 1)
                {
                    other.gameObject.SetActive(false);
                    break;
                }
                else
                {
                    warningText.gameObject.SetActive(true);
                    SetWarningText();
                    break;
                }

            case "Door3":
                if (keyCount == 0)
                {
                    EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
                    break;
                }
                else
                {
                    warningText.gameObject.SetActive(true);
                    SetWarningText();
                    break;
                }

            case "Clue1":
                clue1Text.gameObject.SetActive(true);
                SetClue1Text();
                break;

            case "Clue2":
                clue2Text.gameObject.SetActive(true);
                SetClue2Text();
                break;

            case "Clue3":
                clue3Text.gameObject.SetActive(true);
                SetClue3Text();
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Key":
                keyFoundText.gameObject.SetActive(false);
                break;

            case "Door1":
                warningText.gameObject.SetActive(false);
                break;

            case "Door2":
               warningText.gameObject.SetActive(false);
               break;

            case "Door3":
                warningText.gameObject.SetActive(false);
                break;

            case "Clue1":
                clue1Text.gameObject.SetActive(false);
                break;

            case "Clue2":
                clue2Text.gameObject.SetActive(false);
                break;

            case "Clue3":
                clue3Text.gameObject.SetActive(false);
                break;
        }
    }
}
