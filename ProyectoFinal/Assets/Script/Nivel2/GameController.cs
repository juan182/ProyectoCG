using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController: MonoBehaviour
{
    public FishManager fishManager;
    public GameObject player;
    private Escape escapeScript;

    private BoatMovement boatMovement;

    public GameObject PanelPerdiste;
    public GameObject PanelGanaste;
    public GameObject PanelMinijuego;
    public GameObject PanelAlertaTiburon;
    public TextMeshProUGUI conteo;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        boatMovement = player.GetComponent<BoatMovement>();
        escapeScript = player.GetComponent<Escape>();

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

    public void PanelDeEscape()
    {
        PanelAlertaTiburon.SetActive(false);
        PanelMinijuego.SetActive(true);
        PanelPerdiste.SetActive(false);
        PanelGanaste.SetActive(false);

        boatMovement.ActivarMovimiento(false);
    }

    public void TerminarEscape(bool exito)
    {
        escapeScript.TerminarEscape(); 
        boatMovement.ActivarMovimiento(true);

        foreach (var tiburon in FindObjectsOfType<FishEnemy>())
        {
            tiburon.DesactivarAtaque(4f);
        }

        if (exito)
        {
            GanasteUI();
        }
        else
        {
            PerdisteUI();
        }
    }

    public void PerdisteUI()
    {
        PanelPerdiste.SetActive(true);
        PanelGanaste.SetActive(false);
        PanelAlertaTiburon.SetActive(false);
        PanelMinijuego.SetActive(false);
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
