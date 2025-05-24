using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script que controla el comportamiento de una moneda de oro en el juego.
/// </summary>
public class GoldCoin : MonoBehaviour
{
    private int point = 50;

    public AudioClip audioGold;

    /// <summary>
    /// Detecta la colisión con el jugador para sumar puntos de moneda de oro,
    /// reproduce un sonido si está asignado y destruye el objeto moneda.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.sumGoldenCoin(point);
            if (audioGold != null)
            {
                AudioSource.PlayClipAtPoint(audioGold, Camera.main.transform.position);
            }
        }
        Destroy(gameObject);
    }
}
