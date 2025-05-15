using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FishPatrol : MonoBehaviour
{
    public float distanciaCambioPunto = 1f;
    public Transform[] puntosPatrulla;
    private int indiceActual = 0;
    private NavMeshAgent agente;

    void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (puntosPatrulla.Length > 0)
        {

            Destino();
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (puntosPatrulla.Length == 0) return;

        if (!agente.pathPending && agente.remainingDistance < 0.5f)
        {
            Destino();
        }
    }

    private void Destino()
    {
        if (puntosPatrulla.Length == 0) return;
        indiceActual = Random.Range(0, puntosPatrulla.Length);
        agente.SetDestination(puntosPatrulla[indiceActual].position);
    }

    public void PuntosPatrulla(Transform[] puntos)
    {
        puntosPatrulla = puntos;
        Destino();
    }
}
