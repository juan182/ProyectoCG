using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla la funcionalidad del menú de pausa. 
/// Permite pausar y reanudar el juego
/// mediante la tecla Escape o desde un botón.
/// </summary>
public class MenuPausa : MonoBehaviour
{
    /// <summary>
    /// Panel del menú de pausa que se activa o desactiva.
    /// </summary>
    public GameObject menuPausa;


    // Update is called once per frame

    /// <summary>
    /// Detecta si se presiona la tecla Escape para pausar o reanudar el juego.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausa();
        }


    }

    /// <summary>
    /// Activa o desactiva el menú de pausa y detiene o reanuda el tiempo del juego.
    /// </summary>
    public void pausa()
    {
        if (menuPausa.activeSelf)
        {
            menuPausa.SetActive(false);
            Time.timeScale = 1f; // Reanudar el juego
        }
        else
        {
            menuPausa.SetActive(true);
            Time.timeScale = 0f; // Pausar el juego
        }
    }

}
