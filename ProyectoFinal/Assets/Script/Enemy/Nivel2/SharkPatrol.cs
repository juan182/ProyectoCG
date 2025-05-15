using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SharkPatrol : MonoBehaviour
{

    [SerializeField]
    private Transform[] puntosPatrulla;

    private Transform player;

    private int currentPointIndex = 0;
    private bool persecucion = false;
    [SerializeField]
    private bool espera = false;

    private NavMeshAgent agent;
    private GameController gc;



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        gc = FindObjectOfType<GameController>();

        if (puntosPatrulla.Length > 0)
        {
            agent.SetDestination(puntosPatrulla[currentPointIndex].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (espera) return;

        if (persecucion)
        {
            if (player != null)
                agent.SetDestination(player.position);
            return;
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPointIndex = (currentPointIndex + 1) % puntosPatrulla.Length;
            agent.SetDestination(puntosPatrulla[currentPointIndex].position);
        }
    }

    public void DetenerPorColision()
    {
        espera = true;
        agent.isStopped = true;
    }

    public void ReanudarPatrulla()
    {
        espera = false;
        agent.isStopped = false;
    }

    public void Persecucion(Transform plr)
    {
        persecucion = true;
        player = plr;
        agent.SetDestination(plr.position);
        gc.AlertaTiburon();
    }

    public void SalirPersecucion()
    {
        persecucion = false;
        player = null;

        currentPointIndex = (currentPointIndex + 1) % puntosPatrulla.Length;
        agent.SetDestination(puntosPatrulla[currentPointIndex].position);

        gc.SalirAtaque();
    }

}
