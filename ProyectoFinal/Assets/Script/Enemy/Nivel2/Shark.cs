using UnityEngine;


/// <summary>
/// Controla el comportamiento del tiburón en el juego, incluyendo la detección 
/// de colisiones con el jugador, activación de la persecución y manejo del 
/// cooldown para evitar múltiples colisiones consecutivas.
/// </summary>
/// <example>
/// El tiburón perseguirá al jugador al colisionar y se activará un cooldown 
/// de 4 segundos para evitar nuevas colisiones inmediatas.
/// </example>
public class Shark : MonoBehaviour
{
    private GameController gc;
    private SharkPatrol patrulla;
    private bool colision = false;
    private bool cooldownActivo = false;
    private int sharks = 10;

    /// <summary>
    /// Inicializa las referencias al GameController y al componente SharkPatrol.
    /// </summary>
    private void Start()
    {
        gc = FindObjectOfType<GameController>();
        patrulla = GetComponent<SharkPatrol>();
    }

    /// <summary>
    /// Detecta la colisión con el jugador, suma puntos, activa la persecución
    /// y el minijuego asociado, y comienza el cooldown para evitar colisiones
    /// múltiples.
    /// </summary>
    /// <param name="collision">Información del objeto que colisiona.</param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && !colision && !cooldownActivo)
        {
            colision = true;
            cooldownActivo = true;
            GameManager.Instance.sumShark(sharks);

            Transform playerTransform = collision.transform;
            patrulla.Persecucion(playerTransform);
            gc.MiniJuego(this.GetComponent<SharkPatrol>());

            StartCoroutine(ResetColision());
        }
    }

    /// <summary>
    /// Corrutina que espera 4 segundos para resetear las variables de 
    /// colisión y cooldown.
    /// </summary>
    private System.Collections.IEnumerator ResetColision()
    {
        yield return new WaitForSeconds(4f); // mismo tiempo que el cooldown
        colision = false;
        cooldownActivo = false;
    }
}
