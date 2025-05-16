using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController: MonoBehaviour
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
    private TextMeshProUGUI conteo;
    [SerializeField]
    private TextMeshProUGUI tiburones;

    private bool minijuegoActivo = false;
    private int tiburonConteo;

    private SharkPatrol tiburonActual;

    [SerializeField] 
    private SharkAttackManager sharkManager;
    private void Awake()
    {
        tiburonConteo=0;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void AlertaTiburon()
    {
        
        PanelAlertaTiburon.SetActive(true);
        PanelMinijuego.SetActive(false);
        PanelPerdiste.SetActive(false);
        PanelGanaste.SetActive(false);
    }

    public void Contador(int value)
    {
        
        conteo.text =value.ToString();
    }

    public void TiburonConteo()
    {
        tiburonConteo++;
        tiburones.text = tiburonConteo.ToString();
    }

    public void MiniJuego(SharkPatrol tiburon)
    {
        if (minijuegoActivo) return;

        PanelMinijuego.SetActive(true);
        PanelAlertaTiburon.SetActive(false);
        tiburonActual = tiburon;
        minijuegoActivo = true;
        minijuego.SetActive(true);
        Contador(0);

        MiniJuego scriptMJ = minijuego.GetComponent<MiniJuego>();
        if (scriptMJ != null)
            scriptMJ.InicioMiniJuego();
    }


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
    public void SalirAtaque()
    {
        PanelPerdiste.SetActive(false);
        PanelGanaste.SetActive(false);
        PanelAlertaTiburon.SetActive(false);
        PanelMinijuego.SetActive(false);
    }

    public void PerdisteUI()
    {
        PanelPerdiste.SetActive(true);
        PanelGanaste.SetActive(false);
        PanelAlertaTiburon.SetActive(false);
        PanelMinijuego.SetActive(false);

        ReiniciarEscena();
    }

    public void GanasteUI()
    {
        PanelPerdiste.SetActive(false);
        PanelGanaste.SetActive(true);
        PanelAlertaTiburon.SetActive(false);
        PanelMinijuego.SetActive(false);

        StartCoroutine(DesactivarPanel(PanelGanaste, 2f));
    }

    private IEnumerator DesactivarPanel(GameObject panel, float segundos)
    {
        yield return new WaitForSeconds(segundos);
        panel.SetActive(false);
    }

    public void ReiniciarEscena(float delay = 2f)
    {
        StartCoroutine(Reiniciar(delay));
    }

    private IEnumerator Reiniciar(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
