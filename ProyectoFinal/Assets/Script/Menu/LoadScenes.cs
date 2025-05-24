using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Gestiona la carga de escenas y el cierre del juego.
/// </summary>
/// <example>
/// Este script se asigna a botones UI para cambiar de escena o cerrar el juego.
/// Por ejemplo, desde un botón se puede llamar a LoadScene("Nivel2") o 
/// CloseGame().
/// </example>
public class LoadScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //Cargar scene 

    /// <summary>
    /// Carga la escena especificada por su nombre. Si existe un temporizador, 
    /// lo inicia.
    /// </summary>
    /// <param name="nameScene">Nombre de la escena a cargar.</param>
    public void LoadScene(string nameScene)
    {
        if (Timer.Instance != null)
        {
            Timer.Instance.TimerStart();
        }
        SceneManager.LoadScene(nameScene);

    }

    /// <summary>
    /// Cierra la aplicación.
    /// </summary>
    public void CloseGame()
    {
        Application.Quit();
    }
}