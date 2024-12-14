using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class gameplayMechanics : MonoBehaviour
{
    public int Counter = 0;
    public float Timer;
    public float seconds;

   
    public float defaultTimer = 5f;
    public float readyTimer = 3f;
    public bool readyEnded = false;

    public AudioSource keyaudioSource;
    public gameplayUI UIreference;
  


    void Start()
    {
        
        Timer = readyTimer;
        seconds = Mathf.FloorToInt(Timer);
        keyaudioSource.volume = 1.0f;


    }

    public void pauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {

        if(UIreference.play.activeSelf){
            if (!readyEnded)
            {
                // Countdown from 3 to 1
                Timer -= Time.deltaTime;
                seconds = Mathf.CeilToInt(Timer);
                UIreference.updateTimer();  // Display countdown timer

                if (seconds <= 0)
                {
                    // Countdown has finished
                    readyEnded = true;
                    Timer = defaultTimer;  // Start the main game timer
                }
            }
            else
            {
                // Main game timer
                Timer -= Time.deltaTime;
                seconds = Mathf.CeilToInt(Timer);
                UIreference.updateTimer();  // Display main game timer

                if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
                {
                    Counter++;
                    keyaudioSource.Play();
                    UIreference.updateTapCounter();
                    Debug.Log("Tapped");
                }

                // Check for game over or game won conditions only when main timer ends
                if (seconds <= 0)
                {
                    if (Counter < 20)
                    {
                        if(Counter>int.Parse(PlayerPrefs.GetString("highscore","0")))
                            PlayerPrefs.SetString("highscore",Counter.ToString());
                        UIreference.gameovershow();
                    }
                    else
                    {
                        if (Counter > int.Parse(PlayerPrefs.GetString("highscore", "0")))
                            PlayerPrefs.SetString("highscore", Counter.ToString());
                        UIreference.gameWon();
                    }
                }
            }

        }
        
    }
}
