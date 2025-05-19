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

    [SerializeField]
    private TextMeshProUGUI txtHeart;

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

    public void GuardarTiempoEscena()
    {
        Timer.Instance.TimerStop();

        float tiempoEscena = Timer.Instance.GetElapsedTimeRaw();
        Debug.Log($"Guardando tiempo escena '{nombreEscena}': {tiempoEscena} segundos");

        GameManager.Instance.GuardarTiemposEscenas(nombreEscena, tiempoEscena);

        Timer.Instance.TimerReset();
    }

}
