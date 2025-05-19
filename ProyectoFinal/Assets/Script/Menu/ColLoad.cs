using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColLoad : MonoBehaviour
{
    public string nombreEscena; 

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
