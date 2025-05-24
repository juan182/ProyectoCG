using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController1 : MonoBehaviour, InterfaceTiempoEscena
{
   
    private TextMeshProUGUI partesCarretilla;

    private string nombreEscena;

    // Start is called before the first frame update


    /// <summary>
    /// Se ejecuta al iniciar la escena. 
    /// Guarda el nombre de la escena activa y arranca el temporizador global.
    /// </summary>
    void Start()
    {
        nombreEscena = SceneManager.GetActiveScene().name;
        Timer.Instance.TimerStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Detiene el temporizador, guarda el tiempo transcurrido
    /// para la escena actual usando el GameManager, y reinicia el temporizador.
    /// </summary>
    /// <example>
    /// Por ejemplo, al terminar una escena y antes de cargar la siguiente, 
    /// guarda el tiempo así:
    /// <code>
    /// Guarda el tiempo de la escena actual antes de cambiar de escena:
    /// controlador.GuardarTiempoEscena();
    /// </code>
    /// </example>
    public void GuardarTiempoEscena()
    {
        Timer.Instance.TimerStop();

        float tiempoEscena = Timer.Instance.GetElapsedTimeRaw();
        GameManager.Instance.GuardarTiemposEscenas(nombreEscena, tiempoEscena);

        Timer.Instance.TimerReset();
    }
}
