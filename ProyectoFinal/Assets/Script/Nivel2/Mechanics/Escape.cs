using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    public float pulsaciones=5f;
    private bool escapando = false;
    private float contadorPulsaciones = 0f;

    public float tiempoMaxEscape = 3f;
    private float temporizador = 0f;

    private BoatMovement boatMove;
    private BoteFlota boteFlota;
    private ReinicioEscena reinicioEscena;

    public void InicioEscape()
    {
            
            escapando = true;
            contadorPulsaciones = 0f;
            temporizador = 0f;
            
        // Desactivar movimiento y flotación
            if (boatMove != null) boatMove.ActivarMovimiento(false);
            boatMove.CongelarInclinacion();
    }

    


    // Start is called before the first frame update
    void Start()
    {
        boatMove = GetComponent<BoatMovement>();
        boteFlota = GetComponent<BoteFlota>();
        reinicioEscena = GetComponent<ReinicioEscena>();
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
            
        }

        if (contadorPulsaciones >= pulsaciones)
        {
            TerminarEscape();
            return;
        }

        if (temporizador >= tiempoMaxEscape)
        {
            TerminarEscape();
            Perdiste();
        }
    }
    private void TerminarEscape()
    {
        escapando = false;
        boatMove.ActivarMovimiento(true);
        boatMove.RestaurarInclinacion();
        contadorPulsaciones = 0;
    }

    private void Perdiste()
    {
        Debug.Log("Has perdido!!");

        if (boteFlota != null)
        {
            boteFlota.enabled = false;
        }
            

        if (boatMove != null)
        {
            boatMove.RestaurarInclinacion();
        }
            

        StartCoroutine(Hundirse());

    }

    private IEnumerator Hundirse()
    {
        float espera = 2.5f;
        yield return new WaitForSeconds(espera);

        if (reinicioEscena != null)
        {
            reinicioEscena.ReiniciarEscena();
        }

        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

}
