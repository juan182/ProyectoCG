using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Heart : MonoBehaviour
{
    /// <summary>
    /// Vida que otorga al jugador.
    /// </summary>
    public int cantidadDeVida = 2;

    /// <summary>
    /// Sonido al recolectar el corazón.
    /// </summary>
    public AudioClip audioHeart;

    /// <summary>
    /// Cura al jugador, actualiza el GameManager y destruye el objeto.
    /// </summary>
    /// <param name="other">Collider que entra en contacto.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth salud = other.GetComponent<PlayerHealth>();
            if (salud != null)
            {
                salud.Curar(cantidadDeVida);
            }
            GameManager.Instance.sumHealth(cantidadDeVida);
            Destroy(gameObject);
            if (audioHeart != null)
            {
                AudioSource.PlayClipAtPoint(audioHeart, Camera.main.transform.position);
            }
        }
    }
}
