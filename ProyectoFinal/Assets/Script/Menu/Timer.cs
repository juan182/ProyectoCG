using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    public TextMeshProUGUI timerMinutes;
    public TextMeshProUGUI timerSeconds;
    public TextMeshProUGUI timerSeconds100;

    private float startTime;
    private float stopTotalTime;
    private bool isRunning = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;



        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Use this for initialization
    void Start()
    {
        if (!isRunning)
        {
            // TimerStart();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Busca los textos del timer en cada nueva escena por nombre
        timerMinutes = GameObject.Find("txtMinutos")?.GetComponent<TextMeshProUGUI>();
        timerSeconds = GameObject.Find("txtSegundos")?.GetComponent<TextMeshProUGUI>();
        timerSeconds100 = GameObject.Find("txtMiliSegundos")?.GetComponent<TextMeshProUGUI>();
    }

    public void TimerStart()
    {
        if (!isRunning)
        {
            isRunning = true;
            startTime = Time.time;
            Debug.Log("Timer START: " + startTime);
        }
    }

    public void TimerStop()
    {
        if (isRunning)
        {
            float elapsed = Time.time - startTime;
            print("STOP");
            stopTotalTime += elapsed;
            isRunning = false;
            Debug.Log("Timer STOP. Total acumulado: " + stopTotalTime);
        }
    }

    public void TimerReset()
    {
        print("RESET");
        stopTotalTime = 0;
        isRunning = false;
        startTime = 0;

        if (timerMinutes != null) timerMinutes.text = "00";
        if (timerSeconds != null) timerSeconds.text = "00";
        if (timerSeconds100 != null) timerSeconds100.text = "00";

    }

    // Update is called once per frame
    void Update()
    {


        if (isRunning)
        {
            float currentTime = stopTotalTime + (Time.time - startTime);
            int minutesInt = (int)(currentTime / 60);
            int secondsInt = (int)(currentTime % 60);
            int seconds100Int = (int)((currentTime - (minutesInt * 60 + secondsInt)) * 100);

            if (timerMinutes != null) timerMinutes.text = minutesInt.ToString("00");
            if (timerSeconds != null) timerSeconds.text = secondsInt.ToString("00");
            if (timerSeconds100 != null) timerSeconds100.text = seconds100Int.ToString("00");


        }
    }

    public string GetElapsedTime()
    {
        float currentTime = isRunning ? stopTotalTime + (Time.time - startTime) : stopTotalTime;
        int minutes = (int)(currentTime / 60);
        int seconds = (int)(currentTime % 60);
        int seconds100 = (int)((currentTime - (minutes * 60 + seconds)) * 100);
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, seconds100);
    }

    public float GetElapsedTimeRaw()
    {
        return isRunning ? stopTotalTime + (Time.time - startTime) : stopTotalTime;
    }
}
