using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controlador principal para la escena 2, encargado de iniciar el temporizador y guardar el tiempo transcurrido
/// en la escena por el jugador.
/// </summary>
public class GameController : MonoBehaviour, InterfaceTiempoEscena
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject minijuego;
    [SerializeField]
    private GameObject PanelPerdiste;
    [SerializeField]
    private GameObject PanelGanaste;
    [SerializeField]
    private GameObject PanelMinijuego;
    [SerializeField]
    private GameObject PanelAlertaTiburon;
    [SerializeField]
    private GameObject PanelTiempo;
    [SerializeField]
    private TextMeshProUGUI conteo;
    [SerializeField]
    private TextMeshProUGUI tiburones;
    [SerializeField]
    private TextMeshProUGUI cuentaRegresiva;

    private float cuentaRegresivaTiempo = 60f;
    private float currentTime;
    private bool cuentaRegresivaActiva = true;
    private bool minijuegoActivo = false;

    private SharkPatrol tiburonActual;

    [SerializeField]
    private SharkAttackManager sharkManager;

    private string nombreEscena;


    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = cuentaRegresivaTiempo;
        UpdateCountdownUI(currentTime);

        nombreEscena = SceneManager.GetActiveScene().name;
        Timer.Instance.TimerStart();
    }

    // Update is called once per frame
    /// <summary>
    /// Actualiza la cuenta regresiva cada frame y, activa el panel y reinicia la escena al finalizar el tiempo.
    /// </summary>
    void Update()
    {
        if (cuentaRegresivaActiva)
        {
            currentTime -= Time.deltaTime;

            if (currentTime > 0)
            {
                UpdateCountdownUI(currentTime);
            }
            else
            {
                cuentaRegresivaActiva = false;
                currentTime = 0;
                UpdateCountdownUI(currentTime);
                PanelTiempo.SetActive(true);
                ReiniciarEscena();
                
            }
        }
    }
    /// <summary>
    /// Actualiza el TextMeshPro cuentaRegresiva de la interfaz 
    /// con el tiempo restante en segundos.
    /// </summary>
    /// <param name="time">Tiempo actual restante.</param>
    void UpdateCountdownUI(float time)
    {
        
        int seconds = Mathf.FloorToInt(time % 60);
        

        cuentaRegresiva.text = string.Format("{00}", seconds);
    }

    /// <summary>
    /// Activa el panel de alerta de tiburón y oculta otros paneles del juego.
    /// </summary>
    public void AlertaTiburon()
    {
        
        PanelAlertaTiburon.SetActive(true);
        PanelMinijuego.SetActive(false);
        PanelPerdiste.SetActive(false);
        PanelGanaste.SetActive(false);
        PanelTiempo.SetActive(false);
    }

    /// <summary>
    /// Actualiza el contador de UI con un valor entero determinado.
    /// </summary>
    /// <param name="value">Valor a mostrar en el contador.</param>
    public void Contador(int value)
    {
        
        conteo.text =value.ToString();
    }

    /// <summary>
    /// Muestra en la UI el número de tiburones atrapados usando 
    /// el contador del GameManager.
    /// </summary>
    public void TiburonConteo()
    {
        //tiburonConteo++;
        tiburones.text = GameManager.Instance.shark.ToString();
    }

    /// <summary>
    /// Activa el panel del minijuego y comienza la persecución del tiburón.
    /// </summary>
    /// <param name="tiburon">Referencia al tiburón que está atacando.</param>
    public void MiniJuego(SharkPatrol tiburon)
    {
        if (minijuegoActivo) return;

        PanelMinijuego.SetActive(true);
        PanelAlertaTiburon.SetActive(false);
        tiburonActual = tiburon;
        minijuegoActivo = true;
        minijuego.SetActive(true);
        //Contador(0);

        MiniJuego scriptMJ = minijuego.GetComponent<MiniJuego>();
        if (scriptMJ != null)
            scriptMJ.InicioMiniJuego();
    }

    /// <summary>
    /// Finaliza el minijuego de escape. Muestra la UI de éxito o 
    /// fracaso y reinicia los comportamientos del tiburón.
    /// </summary>
    /// <param name="exito">Verdadero si el jugador escapó, falso 
    /// si falló.</param>
    public void TerminarEscape(bool exito)
    {
        if (!minijuegoActivo) return;

        minijuegoActivo = false;
        minijuego.SetActive(false);

        if (tiburonActual != null)
        {
            tiburonActual.SalirPersecucion();
            tiburonActual = null;
        }

        if (exito)
        {
            TiburonConteo();
            GanasteUI();

        }
        else
        {
            PerdisteUI();
        }

        if (sharkManager != null)
            sharkManager.ReanudarAtaqueYPatrulla();
    }

    /// <summary>
    /// Oculta todos los paneles relacionados con el ataque del tiburón.
    /// </summary>
    public void SalirAtaque()
    {
        PanelPerdiste.SetActive(false);
        PanelGanaste.SetActive(false);
        PanelAlertaTiburon.SetActive(false);
        PanelMinijuego.SetActive(false);
        PanelTiempo.SetActive(false);
    }

    /// <summary>
    /// Muestra el panel de derrota y reinicia la escena.
    /// </summary>
    public void PerdisteUI()
    {
        PanelPerdiste.SetActive(true);
        PanelGanaste.SetActive(false);
        PanelAlertaTiburon.SetActive(false);
        PanelMinijuego.SetActive(false);
        PanelTiempo.SetActive(false);

        ReiniciarEscena();
    }

    /// <summary>
    /// Muestra el panel de victoria y lo oculta automáticamente 
    /// después de un tiempo.
    /// </summary>
    public void GanasteUI()
    {
        PanelPerdiste.SetActive(false);
        PanelGanaste.SetActive(true);
        PanelAlertaTiburon.SetActive(false);
        PanelMinijuego.SetActive(false);
        PanelTiempo.SetActive(false);

        StartCoroutine(DesactivarPanel(PanelGanaste, 2f));
    }


    /// <summary>
    /// Espera unos segundos y luego desactiva un panel en específico.
    /// </summary>
    /// <param name="panel">Panel que se quiere ocultar.</param>
    /// <param name="segundos">Tiempo de espera antes de ocultar el 
    /// panel.</param>
    private IEnumerator DesactivarPanel(GameObject panel, float segundos)
    {
        yield return new WaitForSeconds(segundos);
        panel.SetActive(false);
    }

    /// <summary>
    /// Reinicia la escena actual tras una breve espera.
    /// </summary>
    /// <param name="delay">Espera 2 segundos antes de reinciar la 
    /// escena.</param>
    public void ReiniciarEscena(float delay = 2f)
    {
        StartCoroutine(Reiniciar(delay));
    }


    private IEnumerator Reiniciar(float segundos)
    {
        GameManager.Instance.ResetShark();
        yield return new WaitForSeconds(segundos);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Detiene el temporizador de la escena 2, guarda el tiempo 
    /// en el GameManager y reinicia el cronómetro.
    /// </summary>
    public void GuardarTiempoEscena()
    {
        Timer.Instance.TimerStop();

        float tiempoEscena = Timer.Instance.GetElapsedTimeRaw();
        GameManager.Instance.GuardarTiemposEscenas(nombreEscena, tiempoEscena);

        Timer.Instance.TimerReset();
    }

}
