using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Controla la transición de escenas cuando el jugador entra en un objeto con trigger.
/// Busca un objeto que implemente la interfaz InterfaceTiempoEscena 
/// para guardar el tiempo y luego carga la siguiente escena especificada.
/// </summary>
public class ColLoad : MonoBehaviour
{
    /// <summary>
    /// Nombre de la escena que se va a cargar al entrar en el trigger.
    /// </summary>
    public string nombreEscena;


    /// <summary>
    /// Detecta si el jugador entra en el trigger. Si es así, 
    /// intenta guardar el tiempo utilizando un componente 
    /// que implemente InterfaceTiempoEscena, y luego carga la nueva escena.
    /// </summary>
    /// <param name="other">Collider que entra en el trigger.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InterfaceTiempoEscena controlador = null;

            // Buscar todos los MonoBehaviours y encontrar uno que implemente InterfaceTiempoEscena
            MonoBehaviour[] todos = FindObjectsOfType<MonoBehaviour>();
            foreach (var mono in todos)
            {
                if (mono is InterfaceTiempoEscena)
                {
                    controlador = mono as InterfaceTiempoEscena;
                    break;
                }
            }

            if (controlador != null)
            {
                Debug.Log("Controlador con InterfaceTiempoEscena encontrado. Guardando tiempo...");

                controlador.GuardarTiempoEscena();
            }
            else
            {
                Debug.LogWarning("No se encontró un controlador que implemente InterfaceTiempoEscena.");
            }

            CargarNuevaEscena();
        }
    }

    /// <summary>
    /// Guarda el tiempo de la escena actual en el GameManager y 
    /// carga la nueva escena. Si la nueva escena es "Score", también 
    /// detiene el temporizador.
    /// </summary>
    void CargarNuevaEscena()
    {
        // Guardar el tiempo de la escena actual antes de cargar la siguiente escena
        GameManager.Instance.GuardarTiemposEscenas(SceneManager.GetActiveScene().name, Timer.Instance.GetElapsedTimeRaw());

        // Si vas a cargar la escena Score, detén el timer
        if (nombreEscena == "Score")
        {
            Timer.Instance.TimerStop();
        }

        SceneManager.LoadScene(nombreEscena);
    }
}
