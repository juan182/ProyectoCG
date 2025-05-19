using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController1 : MonoBehaviour, InterfaceTiempoEscena
{
    [SerializeField]
    private TextMeshProUGUI partesCarretilla;

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
        ShowCarretillas();
    }

    public void ShowCarretillas()
    {
        partesCarretilla.text = GameManager.Instance.carretilla.ToString();
    }

    public void GuardarTiempoEscena()
    {
        Timer.Instance.TimerStop();

        float tiempoEscena = Timer.Instance.GetElapsedTimeRaw();
        GameManager.Instance.GuardarTiemposEscenas(nombreEscena, tiempoEscena);

        Timer.Instance.TimerReset();
    }
}
