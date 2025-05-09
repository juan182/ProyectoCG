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
            CargarNuevaEscena();
        }
    }

    void CargarNuevaEscena()
    {
        SceneManager.LoadScene(nombreEscena);
    }
}
