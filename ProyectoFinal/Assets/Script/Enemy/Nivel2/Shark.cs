using UnityEngine;

public class Shark : MonoBehaviour
{
    private GameController gc;
    private SharkPatrol patrulla;
    private bool colision = false;
    private bool cooldownActivo = false;

    private void Start()
    {
        gc = FindObjectOfType<GameController>();
        patrulla = GetComponent<SharkPatrol>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && !colision && !cooldownActivo)
        {
            colision = true;
            cooldownActivo = true;

            Transform playerTransform = collision.transform;
            patrulla.Persecucion(playerTransform);
            gc.MiniJuego(this.GetComponent<SharkPatrol>());

            StartCoroutine(ResetColision());
        }
    }

    private System.Collections.IEnumerator ResetColision()
    {
        yield return new WaitForSeconds(4f); // mismo tiempo que el cooldown
        colision = false;
        cooldownActivo = false;
    }
}
