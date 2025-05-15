using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController: MonoBehaviour
{
   
    public GameObject player;

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
