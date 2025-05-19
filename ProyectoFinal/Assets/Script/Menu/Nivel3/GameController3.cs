using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController3 : MonoBehaviour, InterfaceTiempoEscena
{
    [SerializeField]
    private TextMeshProUGUI txtCopperCoin;

    [SerializeField]
    private TextMeshProUGUI txtSilverCoin;

    [SerializeField]
    private TextMeshProUGUI txtGoldCoin;

    private string nombreEscena;

    // Start is called before the first frame update
    void Start()
    {

        nombreEscena = SceneManager.GetActiveScene().name;
        Timer.Instance.TimerStart();
    }

    // Update is called once per frame
    void Update()
    {
        ShowCopperCoin();
        ShowSilveCoin();
        ShowGoldCoin();
    }

    public void ShowCopperCoin()
    {
        txtCopperCoin.text = GameManager.Instance.copper.ToString();
    }
    public void ShowSilveCoin()
    {
        txtCopperCoin.text = GameManager.Instance.silver.ToString();
    }
    public void ShowGoldCoin()
    {
        txtCopperCoin.text = GameManager.Instance.gold.ToString();
    }

    public void GuardarTiempoEscena()
    {
        Timer.Instance.TimerStop();

        float tiempoEscena = Timer.Instance.GetElapsedTimeRaw();
        Debug.Log($"Guardando tiempo escena '{nombreEscena}': {tiempoEscena} segundos");

        GameManager.Instance.GuardarTiemposEscenas(nombreEscena, tiempoEscena);

        Timer.Instance.TimerReset();
    }

}
