using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartNivel2 : MonoBehaviour
{
    private bool yaReiniciando = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !yaReiniciando)
        {
            yaReiniciando = true;
            Debug.Log("La canoa tocó la zona de hundimiento. Game Over.");
            StartCoroutine(Reiniciar());
        }
    }

    private System.Collections.IEnumerator Reiniciar()
    {
        yield return new WaitForSeconds(1.5f); // Espera 1.5 segundos
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
