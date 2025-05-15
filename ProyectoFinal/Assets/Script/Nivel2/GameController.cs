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
    private GameObject zonaAtaque;
    [SerializeField]
    private SharkPatrol patrullaTiburon;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
        PanelPerdiste.SetActive(false);
        PanelGanaste.SetActive(false);

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

    public void MiniJuego()
    {
        PanelAlertaTiburon.SetActive(false);
        PanelMinijuego.SetActive(true);
        PanelPerdiste.SetActive(false);
        PanelGanaste.SetActive(false);

        minijuego.SetActive(true);
        Contador(0);

        MiniJuego scriptMJ = minijuego.GetComponent<MiniJuego>();
        if (scriptMJ != null)
        {
            scriptMJ.InicioMiniJuego();
        }
    }

    public void TerminarEscape(bool exito)
    {
        if (exito == true)
        {
            GanasteUI();
        }
        else
        {
            PerdisteUI();
        }
        minijuego.SetActive(false);

        ReanudarPatrullaje();

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

    public void ReanudarPatrullaje()
    {
        StartCoroutine(ReactivarZonaAtaquePatrullaje());
    }

    private IEnumerator ReactivarZonaAtaquePatrullaje()
    {
        if (zonaAtaque != null)
        {
            zonaAtaque.SetActive(false);
        }

        yield return new WaitForSeconds(3f);

        if (patrullaTiburon != null)
        {
            patrullaTiburon.ReanudarPatrulla();
        }
        if (zonaAtaque != null)
        {
            zonaAtaque.SetActive(true);
        }
    }
}
