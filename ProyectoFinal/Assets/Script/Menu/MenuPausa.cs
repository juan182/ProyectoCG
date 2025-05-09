using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausa();
        }


    }

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
