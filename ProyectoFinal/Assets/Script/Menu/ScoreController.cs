using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [SerializeField]
    private GameObject PanelNombre;

    [SerializeField]
    private GameObject PanelResumen;

    [SerializeField]
    private TMP_InputField Nombre;

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
    private TextMeshProUGUI Hora;

    [SerializeField]
    private TextMeshProUGUI Minutos;

    [SerializeField]
    private TextMeshProUGUI Segundos;


    // Start is called before the first frame update
    void Start()
    {
        
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
    public void ShowHora()
    {
        Hora.text = GameManager.Instance.copper.ToString();
    }
    public void ShowMinutos()
    {
        Minutos.text = GameManager.Instance.copper.ToString();
    }
    public void ShowSegundos()
    {
        Segundos.text = GameManager.Instance.copper.ToString();
    }

    public void VerResumen()
    {
        PanelNombre.SetActive(false);
        PanelResumen.SetActive(true);
    }

    
}
