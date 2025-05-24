using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script que controla el comportamiento de una moneda de plata en el juego.
/// </summary>
public class SilverCoin : MonoBehaviour
{
    private int point = 100;
    public AudioClip audioSilver;

    /// <summary>
    /// Detecta la colisión con el jugador para sumar puntos de moneda de plata,
    /// reproduce un sonido si está asignado y destruye el objeto moneda.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.sumSilverCoin(point);
            if (audioSilver != null)
            {
                AudioSource.PlayClipAtPoint(audioSilver, Camera.main.transform.position);
            }
        }
        Destroy(gameObject);
    }

}
