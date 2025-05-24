using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script que controla el comportamiento de una moneda de cobre en el juego.
/// </summary>
public class CopperCoin : MonoBehaviour
{
    private int point = 10;

    public AudioClip audioCopper;

    /// <summary>
    /// Detecta la colisión con el jugador para sumar puntos de moneda de cobre,
    /// reproduce un sonido si está asignado y destruye el objeto moneda.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.sumCopperCoin(point);

            if (audioCopper != null)
            {
                AudioSource.PlayClipAtPoint(audioCopper, Camera.main.transform.position);
            }

            Destroy(gameObject);
        }
    }
}
