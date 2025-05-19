using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public int cantidadDeVida = 2;

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
        }
    }
}
