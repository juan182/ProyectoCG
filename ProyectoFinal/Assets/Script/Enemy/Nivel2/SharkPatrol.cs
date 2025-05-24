using System.Collections;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// Controla el comportamiento de patrullaje y persecución de un tiburón 
/// utilizando un NavMeshAgent. Puede alternar entre patrullar puntos 
/// predefinidos y perseguir al jugador.
/// </summary>
/// <example>
/// Para iniciar la persecución del jugador desde otro script:
/// <code>
/// sharkPatrol.Persecucion(playerTransform);
/// </code>
/// Para detener y luego reanudar la patrulla:
/// <code>
/// sharkPatrol.DetenerPatrulla();
/// sharkPatrol.ReanudarPatrulla();
/// </code>
/// </example>
public class SharkPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] puntosPatrulla;
    [SerializeField] private float velocidadPatrulla = 3.5f;
    [SerializeField] private float velocidadPersecucion = 6f;

    private NavMeshAgent agent;
    private GameController gc;
    private Transform player;

    private int currentPointIndex = 0;
    private bool persecucion = false;
    private bool espera = false;
    private bool cooldownActivo = false;

    /// <summary>
    /// Inicializa el agente de navegación y comienza el patrullaje si hay 
    /// puntos asignados.
    /// </summary>
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = velocidadPatrulla;
        gc = FindObjectOfType<GameController>();

        if (puntosPatrulla.Length > 0)
            agent.SetDestination(puntosPatrulla[currentPointIndex].position);
    }

    /// <summary>
    /// Actualiza la lógica de patrullaje o persecución según el estado actual 
    /// del tiburón.
    /// </summary>
    void Update()
    {
        if (espera) { agent.isStopped = true; return; }

        if (persecucion && player != null)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            return;
        }
        else agent.isStopped = false;

        if (!agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance &&
            (!agent.hasPath || agent.velocity.sqrMagnitude == 0f))
        {
            Patrulla();
        }
    }

    /// <summary>
    /// Mueve al tiburón al siguiente punto de patrullaje.
    /// </summary>
    public void Patrulla()
    {
        currentPointIndex = (currentPointIndex + 1) % puntosPatrulla.Length;
        agent.SetDestination(puntosPatrulla[currentPointIndex].position);
    }

    /// <summary>
    /// Detiene el patrullaje y la persecución del tiburón.
    /// </summary>
    public void DetenerPatrulla()
    {
        espera = true;
        persecucion = false;
        agent.isStopped = true;
    }

    /// <summary>
    /// Reanuda el patrullaje después de haber sido detenido.
    /// </summary>
    public void ReanudarPatrulla()
    {
        espera = false;
        agent.isStopped = false;
        Patrulla();
    }

    /// <summary>
    /// Inicia la persecución del jugador.
    /// </summary>
    /// <param name="plr">Transform del jugador a perseguir.</param>
    public void Persecucion(Transform plr)
    {
        if (cooldownActivo) return;

        persecucion = true;
        player = plr;
        agent.speed = velocidadPersecucion;
        agent.SetDestination(plr.position);
        gc.AlertaTiburon();
    }

    /// <summary>
    /// Finaliza la persecución y retoma el patrullaje.
    /// </summary>
    public void SalirPersecucion()
    {
        persecucion = false;
        player = null;
        agent.speed = velocidadPatrulla;
        Patrulla();
        gc.SalirAtaque();
        Debug.Log("Sale de la persecusion");
    }

    /// <summary>
    /// Pausa temporalmente la persecución por una duración determinada.
    /// </summary>
    /// <param name="duracion">Duración del cooldown en segundos.</param>
    public void PausarPersecucion(float duracion)
    {
        if (persecucion)
            StartCoroutine(PausarPersecucionCoroutine(duracion));
    }

    /// <summary>
    /// Corrutina que pausa la persecución y patrullaje temporalmente.
    /// </summary>
    /// <param name="duracion">Tiempo de espera antes de reanudar.</param>
    private IEnumerator PausarPersecucionCoroutine(float duracion)
    {
        DetenerPatrulla();
        cooldownActivo = true;

        yield return new WaitForSeconds(duracion);

        espera = false;
        persecucion = false;
        cooldownActivo = false;

        Patrulla();
        agent.isStopped = false;
    }
}
