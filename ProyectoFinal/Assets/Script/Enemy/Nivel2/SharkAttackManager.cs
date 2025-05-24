using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Administra el comportamiento de patrullaje y ataque de un grupo de tiburones.
/// Permite detener y reanudar sus actividades durante un minijuego u otros
/// eventos del juego.
/// </summary>
/// <example>
/// Se puede usar para detener el patrullaje cuando el jugador entra en una zona
/// segura, y reanudarlo después de 7 segundos.
/// <code>
/// sharkAttackManager.DetenerAtaqueYPatrulla();
/// sharkAttackManager.ReanudarAtaqueYPatrulla(7f);
/// </code>
/// </example>
public class SharkAttackManager : MonoBehaviour
{
    [SerializeField]
    private SharkPatrol[] sharkPatrol;
    [SerializeField]
    private GameObject[] zonaAtaque;

    /// <summary>
    /// Inicializa los arrays de patrullaje y activa las zonas de ataque al 
    /// iniciar el juego.
    /// </summary>
    private void Awake()
    {
        
        if (sharkPatrol == null || sharkPatrol.Length == 0)
            sharkPatrol = GetComponentsInChildren<SharkPatrol>();

        
        if (zonaAtaque != null)
        {
            foreach (var zona in zonaAtaque)
                if (zona != null)
                    zona.SetActive(true);
        }
    }

    /// <summary>
    /// Detiene la patrulla de todos los tiburones y desactiva las zonas de ataque.
    /// </summary>
    public void DetenerAtaqueYPatrulla()
    {
        if (zonaAtaque != null)
        {
            foreach (var zona in zonaAtaque)
                if (zona != null)
                    zona.SetActive(false);
        }

        if (sharkPatrol != null)
        {
            foreach (var tiburon in sharkPatrol)
                if (tiburon != null)
                    tiburon.DetenerPatrulla();
        }
    }

    /// <summary>
    /// Reanuda la patrulla de los tiburones y las zonas de ataque después de 
    /// un retraso opcional.
    /// </summary>
    /// <param name="delay">Tiempo en segundos para reanudar las actividades. 
    /// Por defecto, 7 segundos.</param>
    public void ReanudarAtaqueYPatrulla(float delay = 7f)
    {
        StartCoroutine(ReanudarDespuesDeMinijuego(delay));
    }

    /// <summary>
    /// Corrutina que espera un número determinado de segundos antes de 
    /// reactivar las patrullas y zonas de ataque.
    /// </summary>
    /// <param name="segundos">Tiempo a esperar antes de reanudar.</param>
    private IEnumerator ReanudarDespuesDeMinijuego(float segundos)
    {
        yield return new WaitForSeconds(segundos);

        if (sharkPatrol != null)
        {
            foreach (var tiburon in sharkPatrol)
                if (tiburon != null)
                    tiburon.ReanudarPatrulla();
        }

        if (zonaAtaque != null)
        {
            foreach (var zona in zonaAtaque)
                if (zona != null)
                    zona.SetActive(true);
        }
    }
}
