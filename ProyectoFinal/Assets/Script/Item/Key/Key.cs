using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Cambia a la escena "Nivel3" cuando el jugador entra en contacto con la llave.
/// </summary>
/// <example>
/// Este script debe ser asignado a un GameObject con un collider marcado como 
/// Trigger. El jugador debe tener el tag "Player" para que funcione 
/// correctamente.
/// </example>
public class Key : MonoBehaviour
{

    /// <summary>
    /// Detecta si el jugador entra en contacto con la llave y cambia de escena.
    /// </summary>
    /// <param name="other">El collider que entra en contacto con la llave.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Nivel3");
        }
    }

}
