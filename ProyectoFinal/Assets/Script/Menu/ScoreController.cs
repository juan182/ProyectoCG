using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [SerializeField]
    private GameObject PanelNombre;

    [SerializeField]
    private GameObject PanelResumen;

    [SerializeField]
    private GameObject PanelDetalles;

    [SerializeField]
    private TMP_InputField InputFieldNombre;

    [SerializeField]
    private Button Resumen;

    [SerializeField]
    private Button Menu;

    [SerializeField]
    private TextMeshProUGUI Sharks;

    [SerializeField]
    private TextMeshProUGUI GoldCoins;

    [SerializeField]
    private TextMeshProUGUI SilverCoins;

    [SerializeField]
    private TextMeshProUGUI CopperCoins;

    [SerializeField]
    private TextMeshProUGUI Minutos;

    [SerializeField]
    private TextMeshProUGUI Segundos;

    [SerializeField]
    private TextMeshProUGUI Milisegundos;

    [SerializeField]
    private TextMeshProUGUI mensajeError;

    #region TextMeshProDetallesResumen
    [SerializeField] private TextMeshProUGUI minEscena1;
    [SerializeField] private TextMeshProUGUI segEscena1;
    [SerializeField] private TextMeshProUGUI milEscena1;

    [SerializeField] private TextMeshProUGUI minEscena2;
    [SerializeField] private TextMeshProUGUI segEscena2;
    [SerializeField] private TextMeshProUGUI milEscena2;

    [SerializeField] private TextMeshProUGUI minEscena3;
    [SerializeField] private TextMeshProUGUI segEscena3;
    [SerializeField] private TextMeshProUGUI milEscena3;
    #endregion

    private Dictionary<string, (TextMeshProUGUI min, TextMeshProUGUI seg, TextMeshProUGUI mil)> uiPorEscena;

    #region Usuario
    private string nombreUsuario;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        PanelNombre.SetActive(true);
        PanelResumen.SetActive(false);
        PanelDetalles.SetActive(false);

        ShowCopperCoins();
        ShowSilverCoins();
        ShowGoldCoins();
        ShowSharks();

        ConfigurarUI();
        MostrarTiemposPorEscena();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCopperCoins()
    {
        CopperCoins.text = GameManager.Instance.copper.ToString();
    }
    public void ShowSilverCoins()
    {
        SilverCoins.text = GameManager.Instance.silver.ToString();
    }
    public void ShowGoldCoins()
    {
        GoldCoins.text = GameManager.Instance.gold.ToString();
    }
    public void ShowSharks()
    {
        Sharks.text = GameManager.Instance.shark.ToString();
    }

    public void VerResumen()
    {
        nombreUsuario = InputFieldNombre.text.Trim();
        GameManager.Instance.nombreJugador = nombreUsuario.ToString();

        if (string.IsNullOrEmpty(nombreUsuario))
        {
            mensajeError.gameObject.SetActive(true);
        }
        else
        {
            mensajeError.gameObject.SetActive(false);


            PanelNombre.SetActive(false);
            PanelResumen.SetActive(true);
            PanelDetalles.SetActive(false);
        }
    }

    public void VerDetalles()
    {
        PanelNombre.SetActive(false);
        PanelResumen.SetActive(false);
        PanelDetalles.SetActive(true);
    }

    void ConfigurarUI()
    {
        // Mapea la UI de cada escena
        uiPorEscena = new Dictionary<string, (TextMeshProUGUI, TextMeshProUGUI, TextMeshProUGUI)>
        {
            { "Nivel1", (minEscena1, segEscena1, milEscena1) },
            { "Nivel2", (minEscena2, segEscena2, milEscena2) },
            { "Nivel3", (minEscena3, segEscena3, milEscena3) }
        };
    }

    void MostrarTiemposPorEscena()
    {
        Debug.Log("Tiempos guardados en GameManager:");

        float tiempoTotal = 0f;

        foreach (var kvp in GameManager.Instance.tiempoEscenas)
        {
            Debug.Log($"Escena: {kvp.Key} - Tiempo en segundos: {kvp.Value}");

            tiempoTotal += kvp.Value;
        }

        // Ahora asignar a UI el tiempo formateado:
        foreach (var nombreEscena in new string[] { "Nivel1", "Nivel2", "Nivel3" })
        {
            if (GameManager.Instance.tiempoEscenas.TryGetValue(nombreEscena, out float tiempo))
            {
                int minutos = (int)(tiempo / 60);
                int segundos = (int)(tiempo % 60);
                int milisegundos = (int)((tiempo - Mathf.Floor(tiempo)) * 100);

                switch (nombreEscena)
                {
                    case "Nivel1":
                        minEscena1.text = minutos.ToString("00");
                        segEscena1.text = segundos.ToString("00");
                        milEscena1.text = milisegundos.ToString("00");
                        break;
                    case "Nivel2":
                        minEscena2.text = minutos.ToString("00");
                        segEscena2.text = segundos.ToString("00");
                        milEscena2.text = milisegundos.ToString("00");
                        break;
                    case "Nivel3":
                        minEscena3.text = minutos.ToString("00");
                        segEscena3.text = segundos.ToString("00");
                        milEscena3.text = milisegundos.ToString("00");
                        break;
                }
            }
            else
            {
                // Si no hay tiempo guardado, mostrar --
                switch (nombreEscena)
                {
                    case "Nivel1":
                        minEscena1.text = "--";
                        segEscena1.text = "--";
                        milEscena1.text = "--";
                        break;
                    case "Nivel2":
                        minEscena2.text = "--";
                        segEscena2.text = "--";
                        milEscena2.text = "--";
                        break;
                    case "Nivel3":
                        minEscena3.text = "--";
                        segEscena3.text = "--";
                        milEscena3.text = "--";
                        break;
                }
            }
        }

        int minutosTotal = (int)(tiempoTotal / 60);
        int segundosTotal = (int)(tiempoTotal % 60);
        int milisegundosTotal = (int)((tiempoTotal - Mathf.Floor(tiempoTotal)) * 100);

        Minutos.text = minutosTotal.ToString("00");
        Segundos.text = segundosTotal.ToString("00");
        Milisegundos.text = milisegundosTotal.ToString("00");

    }


    public void CloseGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void LoadScene(string nameScene)
    {

        SceneManager.LoadScene("Menu");

    }

}
