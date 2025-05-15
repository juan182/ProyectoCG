using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SharkPatrol : MonoBehaviour
{

    [SerializeField]
    private Transform[] puntosPatrulla;

    private int currentPointIndex = 0;

    private NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (puntosPatrulla.Length > 0)
        {
            agent.SetDestination(puntosPatrulla[currentPointIndex].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPointIndex = (currentPointIndex + 1) % puntosPatrulla.Length;
            agent.SetDestination(puntosPatrulla[currentPointIndex].position);
        }
    }
}
