using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controlador para la escena 3 que muestra en UI los recolectables 
/// (monedas de cobre, plata, oro) y la salud del jugador,
/// maneja el temporizador de la escena y guarda el tiempo jugado 
/// cuando se solicita.
/// </summary>
public class GameController3 : MonoBehaviour, InterfaceTiempoEscena
{
    // Variables para mostrar los valores en la interfaz con TextMeshProUGUI
    [SerializeField]
    private TextMeshProUGUI txtCopperCoin;

    [SerializeField]
    private TextMeshProUGUI txtSilverCoin;

    [SerializeField]
    private TextMeshProUGUI txtGoldCoin;

    [SerializeField]
    private TextMeshProUGUI txtHeart;

    private string nombreEscena;

    // Start is called before the first frame update
    void Start()
    {
        // Obtener el nombre de la escena actual y empezar el temporizador
        nombreEscena = SceneManager.GetActiveScene().name;
        Timer.Instance.TimerStart();
    }

    // Update is called once per frame
    void Update()
    {
        // Actualizar en pantalla los valores de monedas y salud cada frame
        ShowCopperCoin();
        ShowSilverCoin();
        ShowGoldCoin();
        ShowHealth();
    }

    public void ShowHealth()
    {
        txtHeart.text = GameManager.Instance.health.ToString();
    }

    public void ShowCopperCoin()
    {
        txtCopperCoin.text = GameManager.Instance.copper.ToString();
    }
    public void ShowSilverCoin()
    {
        txtSilverCoin.text = GameManager.Instance.silver.ToString();
    }
    public void ShowGoldCoin()
    {
        txtGoldCoin.text = GameManager.Instance.gold.ToString();
    }

    /// <summary>
    /// Implementación de InterfaceTiempoEscena: detiene el temporizador, 
    /// guarda el tiempo jugado en GameManager, y resetea el temporizador 
    /// para la escena siguiente.
    /// </summary>
    public void GuardarTiempoEscena()
    {
        Timer.Instance.TimerStop();

        float tiempoEscena = Timer.Instance.GetElapsedTimeRaw();
        Debug.Log($"Guardando tiempo escena '{nombreEscena}': {tiempoEscena} segundos");

        GameManager.Instance.GuardarTiemposEscenas(nombreEscena, tiempoEscena);

        Timer.Instance.TimerReset();
    }

}
