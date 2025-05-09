using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    public float pulsaciones = 5f;
    private bool escapando = false;
    private int contadorPulsaciones = 0;

    public float tiempoMaxEscape = 3f;
    private float temporizador = 0f;

    private BoatMovement boatMove;
    private BoteFlota boteFlota;

    GameController gameController;

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }


    // Start is called before the first frame update
    void Start()
    {
        boatMove = GetComponent<BoatMovement>();
        boteFlota = GetComponent<BoteFlota>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!escapando) return;

        temporizador += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            contadorPulsaciones++;
            Debug.Log("SPACE detectado, contador = " + contadorPulsaciones);
            gameController.Contador(contadorPulsaciones);
        }

        if (contadorPulsaciones >= pulsaciones)
        {
            gameController.TerminarEscape(true);
            return;
        }

        if (temporizador >= tiempoMaxEscape)
        {
            gameController.TerminarEscape(false);
            Perdiste();
        }
    }

    public bool EstaEscapando()
    {
        return escapando;
    }

    public void InicioEscape()
    {
        escapando = true;
        contadorPulsaciones = 0;
        temporizador = 0f;
        boatMove.CongelarInclinacion();
        Debug.Log("Minijuego iniciado desde escape");
        gameController.PanelDeEscape();
        gameController.Contador(0);
    }

    public void TerminarEscape()
    {
        escapando = false;
        boatMove.RestaurarInclinacion();
        contadorPulsaciones = 0;
    }

    private void Perdiste()
    {
        Debug.Log("Has perdido!!");

        if (boteFlota != null) boteFlota.enabled = false;
        boatMove.RestaurarInclinacion();

        gameController.PerdisteUI();
        gameController.ReiniciarEscena();

    }



}
