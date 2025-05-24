using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int vidaMaxima = 5;
    public int vidaActual;

    // Start is called before the first frame update
    void Start()
    {
        vidaActual = GameManager.Instance.health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Aumenta la vida actual del jugador sin superar el máximo.
    /// También actualiza la vida almacenada en el GameManager.
    /// </summary>
    /// <param name="cantidad">Cantidad de vida a restaurar.</param>
    /// <example>
    /// Por ejemplo:
    /// <code>
    /// PlayerHealth salud = GetComponent<PlayerHealth>();
    /// salud.Curar(2);
    /// </code>
    /// Esto aumentará la vida del jugador en 2 unidades, sin exceder la vida máxima.
    /// </example>
    public void Curar(int cantidad)
    {
        vidaActual += cantidad;
        vidaActual = Mathf.Min(vidaActual, vidaMaxima);
        GameManager.Instance.health = vidaActual;
        Debug.Log("Vida actual: " + vidaActual);
    }


    /// <summary>
    /// Resta vida al jugador y verifica si debe morir.
    /// También actualiza la vida en el GameManager.
    /// </summary>
    /// <param name="cantidad">Cantidad de daño recibido.</param>
    /// <example>
    /// Por ejemplo:
    /// <code>
    /// PlayerHealth salud = GetComponent<PlayerHealth>();
    /// salud.RecibirDaño(3);
    /// </code>
    /// Esto reducirá la vida actual del jugador en 3 unidades. 
    /// Si la vida llega a cero, el jugador morirá y se reiniciará la escena.
    /// </example>
    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;
        GameManager.Instance.health = vidaActual;
        Debug.Log("Vida actual: " + vidaActual);
        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    /// <summary>
    /// Reinicia la escena actual cuando el jugador muere.
    /// </summary>
    /// <example>
    /// Este método es llamado internamente cuando la vida del 
    /// jugador es menor o igual a 0.
    /// </example>
    void Morir()
    {
        Debug.Log("El jugador ha muerto.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
