using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniJuego : MonoBehaviour
{
    private int pulsacionesNecesarias = 8;
    private int pulsacionesActuales;

    private GameController gc;
    private bool activo = false;

    private float tiempoLimiteMJ = 3f;

    private bool terminado = false;

    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

    public void InicioMiniJuego()
    {
        pulsacionesActuales = 0;
        activo = true;
        gc.Contador(pulsacionesActuales);
        StartCoroutine(TiempoLimite());
    }

    // Update is called once per frame
    void Update()
    {
        if (!activo || terminado) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pulsacionesActuales++;
            gc.Contador(pulsacionesActuales);

            if (pulsacionesActuales >= pulsacionesNecesarias)
            {

                gc.TerminarEscape(true);
            
                activo = false;
            }
        }
    }

    private IEnumerator TiempoLimite()
    {
        yield return new WaitForSeconds(tiempoLimiteMJ);

        if (activo && !terminado)
        {
            terminado = true;
            activo = false;
            gc.TerminarEscape(false);
        }
    }

}
