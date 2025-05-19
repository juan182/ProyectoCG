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

    public void Curar(int cantidad)
    {
        vidaActual += cantidad;
        vidaActual = Mathf.Min(vidaActual, vidaMaxima);
        GameManager.Instance.health = vidaActual;
        Debug.Log("Vida actual: " + vidaActual);
    }

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
    void Morir()
    {
        Debug.Log("El jugador ha muerto.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
