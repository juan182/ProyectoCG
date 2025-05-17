using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel2Player : MonoBehaviour
{
    private Animator playerAnimator;

    private void Start()
    {
        // Buscar el Animator del jugador hijo de la canoa
        playerAnimator = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shark"))
        {
            if (playerAnimator != null)
            {
                playerAnimator.SetTrigger("panic");
            }
        }
    }

}
