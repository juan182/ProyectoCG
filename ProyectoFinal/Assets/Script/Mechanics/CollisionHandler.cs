using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;


/// <summary>
/// Maneja la colisión con el jugador para sumar puntos, 
/// reproducir un sonido, activar un objeto y destruir el objeto actual.
/// </summary>
public class CollisionHandler : MonoBehaviour
{
    // Objeto que se activará al colisionar
    public GameObject objectToActivate;

    // Puntos que se suman al GameManager
    public int point = 0;

    // Sonido que se reproduce al colisionar
    public AudioClip clip;


    /// <summary>
    /// Detecta la colisión con el jugador. 
    /// Al colisionar, reproduce un sonido opcional, suma puntos al GameManager,
    /// destruye este objeto y activa un objeto asignado.
    /// </summary>
    /// <param name="other">El collider que entró en contacto.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (clip != null)
            {
                AudioSource.PlayClipAtPoint(clip, transform.position);
            }
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.sumCarretilla(point);
            }
            Destroy(gameObject);

            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }
        }
    }
}
