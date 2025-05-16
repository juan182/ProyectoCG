using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = velocidadPatrulla;
        gc = FindObjectOfType<GameController>();

        if (puntosPatrulla.Length > 0)
            agent.SetDestination(puntosPatrulla[currentPointIndex].position);
    }

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

    public void Patrulla()
    {
        currentPointIndex = (currentPointIndex + 1) % puntosPatrulla.Length;
        agent.SetDestination(puntosPatrulla[currentPointIndex].position);
    }

    public void DetenerPatrulla()
    {
        espera = true;
        persecucion = false;
        agent.isStopped = true;
    }

    public void ReanudarPatrulla()
    {
        espera = false;
        agent.isStopped = false;
        Patrulla();
    }

    public void Persecucion(Transform plr)
    {
        if (cooldownActivo) return;

        persecucion = true;
        player = plr;
        agent.speed = velocidadPersecucion;
        agent.SetDestination(plr.position);
        gc.AlertaTiburon();
    }

    public void SalirPersecucion()
    {
        persecucion = false;
        player = null;
        agent.speed = velocidadPatrulla;
        Patrulla();
        gc.SalirAtaque();
        Debug.Log("Sale de la persecusion");
    }

    public void PausarPersecucion(float duracion)
    {
        if (persecucion)
            StartCoroutine(PausarPersecucionCoroutine(duracion));
    }

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
