using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;


/// <summary>
/// Controla un temporizador persistente entre escenas que acumula tiempo
/// y puede mostrarlo en pantalla al llegar a la escena de puntuación.
/// </summary>
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

    /// <summary>
    /// Elimina el listener de cambio de escena al destruir el objeto.
    /// </summary>
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Use this for initialization
    void Start()
    {

    }

    /// <summary>
    /// Se ejecuta al cargar una nueva escena. 
    /// Si es la escena de puntuación, detiene el temporizador y 
    /// actualiza los textos.
    /// </summary>
    /// <param name="scene">Escena cargada.</param>
    /// <param name="mode">Modo de carga.</param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Score")
        {
            TimerStop(); // Detiene el timer cuando cargas la escena score
            timerMinutes = GameObject.Find("txtMinutos")?.GetComponent<TextMeshProUGUI>();
            timerSeconds = GameObject.Find("txtSegundos")?.GetComponent<TextMeshProUGUI>();
            timerSeconds100 = GameObject.Find("txtMiliSegundos")?.GetComponent<TextMeshProUGUI>();

            string tiempoFormateado = GetElapsedTime();

            // Asignar los valores individuales a los textos
            if (tiempoFormateado.Length >= 8)
            {
                if (timerMinutes != null) timerMinutes.text = tiempoFormateado.Substring(0, 2);     // mm
                if (timerSeconds != null) timerSeconds.text = tiempoFormateado.Substring(3, 2);     // ss
                if (timerSeconds100 != null) timerSeconds100.text = tiempoFormateado.Substring(6, 2); // cs
            }
        }
    }

    /// <summary>
    /// Inicia el temporizador si no está corriendo.
    /// </summary>
    /// <example>
    /// <code>
    /// Timer.Instance.TimerStart();
    /// </code>
    /// </example>
    public void TimerStart()
    {
        if (!isRunning)
        {
            isRunning = true;
            startTime = Time.time;
            Debug.Log("Timer START: " + startTime);
        }
    }

    /// <summary>
    /// Detiene el temporizador y acumula el tiempo transcurrido.
    /// </summary>
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

    /// <summary>
    /// Reinicia el temporizador, eliminando el tiempo acumulado.
    /// </summary>
    public void TimerReset()
    {
        
        stopTotalTime = 0;
        isRunning = false;
        startTime = 0;

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Devuelve el tiempo transcurrido en formato mm:ss:cc 
    /// (minutos, segundos, centésimas).
    /// </summary>
    /// <returns>Cadena con el tiempo transcurrido formateado.</returns>
    /// <example>
    /// <code>
    /// string tiempo = Timer.Instance.GetElapsedTime(); // "01:23:45"
    /// </code>
    /// </example>
    public string GetElapsedTime()
    {
        float currentTime = isRunning ? stopTotalTime + (Time.time - startTime) : stopTotalTime;
        int minutes = (int)(currentTime / 60);
        int seconds = (int)(currentTime % 60);
        int seconds100 = (int)((currentTime - (minutes * 60 + seconds)) * 100);
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, seconds100);
    }

    /// <summary>
    /// Devuelve el tiempo transcurrido como un número decimal (segundos).
    /// </summary>
    /// <returns>Tiempo total en segundos¿.</returns>
    public float GetElapsedTimeRaw()
    {
        return isRunning ? stopTotalTime + (Time.time - startTime) : stopTotalTime;
    }
}
