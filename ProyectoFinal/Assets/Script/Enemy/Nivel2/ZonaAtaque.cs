using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detecta si el jugador entra o sale de la zona de ataque del tiburón.
/// Al ingresar el jugador, inicia la persecución desde el script SharkPatrol.
/// Al salir el jugador, detiene la persecución.
/// </summary>
/// <example>
/// Este script debe colocarse en un GameObject con un collider marcado como 
/// trigger, y debe ser hijo de un GameObject (tiburon) que tenga el script 
/// SharkPatrol.
/// </example>
public class ZonaAtaque : MonoBehaviour
{

    private SharkPatrol scriptPatrulla;


    // Start is called before the first frame update

    /// <summary>
    /// Busca el componente SharkPatrol en el GameObject padre al iniciar.
    /// </summary>
    void Start()
    {
        scriptPatrulla = GetComponentInParent<SharkPatrol>();
    }

    /// <summary>
    /// Si el jugador entra en la zona de ataque, inicia la persecución.
    /// </summary>
    /// <param name="other">Collider que entra en la zona de ataque.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scriptPatrulla.Persecucion(other.transform);
            
        }
    }

    /// <summary>
    /// Si el jugador sale de la zona de ataque, detiene la persecución.
    /// </summary>
    /// <param name="other">Collider que sale de la zona de ataque.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scriptPatrulla.SalirPersecucion();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
