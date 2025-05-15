using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shark : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameController gc;

    private bool colision = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        gc= FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ColisionJugador(collision.gameObject);
        }
    }

    private void ColisionJugador(GameObject player)
    {
        if (colision) return;
        colision = true;

        agent.isStopped = true;

        BoatMovement boat = player.GetComponent<BoatMovement>();
        if (boat != null)
        {
            boat.ActivarMovimiento(false);
            StartCoroutine(ReactivarMovimiento(boat));
        }

        SharkPatrol patrulla = GetComponent<SharkPatrol>();
        if (patrulla != null)
        {
            patrulla.DetenerPorColision();
        }

        gc.MiniJuego();
    }

    public void ResetColision()
    {
        colision = false;
    }

    private IEnumerator ReactivarMovimiento(BoatMovement boat)
    {
        yield return new WaitForSeconds(3f);

        if (boat != null)
        {
            boat.ActivarMovimiento(true);
        }

        if (agent != null)
        {
            agent.isStopped = false;
        }
        colision = false;
    }




}
