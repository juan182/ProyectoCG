using System.Collections;
using UnityEngine;

/// <summary>
/// Mini juego donde el jugador debe presionar espacio
/// varias veces en poco tiempo.
/// </summary>
public class MiniJuego : MonoBehaviour
{
    private int pulsacionesNecesarias = 8; // Cuántas veces se necesita presionar espacio
    private int pulsacionesActuales; // Cuántas veces ha presionado hasta ahora

    private GameController gc;
    private bool activo = false; // Si el mini juego está activo
    private bool terminado = false; // Si ya se terminó el mini juego
    private float tiempoLimiteMJ = 3f; // Tiempo en seg máximo para completar el mini juego

    void Start()
    {
        gc = FindObjectOfType<GameController>(); // Busca el controlador del juego en la escena
    }

    /// <summary>
    /// Comienza el mini juego: resetea los valores de las pulsaciones
    /// y arranca el cronómetro.
    /// </summary>
    public void InicioMiniJuego()
    {
        pulsacionesActuales = 0;
        activo = true;
        terminado = false;
        gc.Contador(pulsacionesActuales); // Actualiza el contador en pantalla
        StartCoroutine(TiempoLimite()); // Empieza el límite de tiempo
    }

    void Update()
    {
        // Si no está activo o ya terminó, no hacer nada
        if (!activo || terminado) return;

        // Si presiona espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pulsacionesActuales++;
            gc.Contador(pulsacionesActuales); // Muestra cuántas pulsaciones lleva

            // Si ya alcanzó la meta
            if (pulsacionesActuales >= pulsacionesNecesarias)
            {
                activo = false;
                terminado = true;
                gc.TerminarEscape(true); // Le avisa al GameController que ganó
            }
        }
    }

    // Cuenta regresiva para perder si no llega a tiempo
    private IEnumerator TiempoLimite()
    {
        yield return new WaitForSeconds(tiempoLimiteMJ);

        if (activo && !terminado)
        {
            terminado = true;
            activo = false;
            gc.TerminarEscape(false); // Le avisa al GameController que perdió
        }
    }
}
