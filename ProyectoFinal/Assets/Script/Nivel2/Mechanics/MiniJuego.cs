using System.Collections;
using UnityEngine;

public class MiniJuego : MonoBehaviour
{
    private int pulsacionesNecesarias = 8;
    private int pulsacionesActuales;

    private GameController gc;
    private bool activo = false;
    private bool terminado = false;
    private float tiempoLimiteMJ = 3f;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

    public void InicioMiniJuego()
    {
        pulsacionesActuales = 0;
        activo = true;
        terminado = false;
        gc.Contador(pulsacionesActuales);
        StartCoroutine(TiempoLimite());
    }

    void Update()
    {
        if (!activo || terminado) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pulsacionesActuales++;
            gc.Contador(pulsacionesActuales);

            if (pulsacionesActuales >= pulsacionesNecesarias)
            {
                activo = false;
                terminado = true;
                gc.TerminarEscape(true);
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
