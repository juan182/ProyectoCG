using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FishEnemy : MonoBehaviour
{
    public float velocidadAtaque = 15f;
    public float distanciaDeteccion = 40f;
    public float tiempoEspera = 2f;

    private Escape escape;
    private Transform canoa;
    private GameController gameController;

    private bool atacando = false;
    private bool regresando = false;
    private float tiempoProximoAtaque = 0f;

    private Vector3 posicionInicioAtaque;

    private FishPatrol fishPatrol;
    private NavMeshAgent agente;

    private void Start()
    {
        canoa = GameObject.FindGameObjectWithTag("Player").transform;
        escape = canoa.GetComponent<Escape>();
        fishPatrol = GetComponent<FishPatrol>();
        agente = GetComponent<NavMeshAgent>();
        gameController = GameObject.FindObjectOfType<GameController>();


        Physics.IgnoreCollision(GetComponent<Collider>(), canoa.GetComponent<Collider>());



    }

    private void Update()
    {
        float distancia = Vector3.Distance(transform.position, canoa.position);

        if (!atacando && !regresando && distancia <= distanciaDeteccion && Time.time > tiempoProximoAtaque)
        {
            atacando = true;
            posicionInicioAtaque = transform.position;

            fishPatrol.enabled = false;
            agente.ResetPath();

            gameController.AlertaTiburon();
        }

        if (atacando)
        {
            // Si el minijuego está activo, quedarse cerca de la canoa y no hacer nada más
            if (escape != null && escape.EstaEscapando())
            {
                agente.SetDestination(canoa.position);
                return;
            }

            Vector3 direccion = (canoa.position - transform.position).normalized;
            agente.Move(direccion * velocidadAtaque * Time.deltaTime);

            if (Vector3.Distance(transform.position, canoa.position) < 3f)
            {
                atacando = false;
                regresando = true;
                tiempoProximoAtaque = Time.time + tiempoEspera;

                Debug.Log("Tiburón inició el minijuego (colisión detectada)");
                escape.InicioEscape();
                gameController.PanelDeEscape();
            }
        }

        // Si ya no está atacando pero debe regresar
        if (regresando && (escape == null || !escape.EstaEscapando()))
        {
            NavMeshHit hit;

            if (NavMesh.SamplePosition(posicionInicioAtaque, out hit, 2.0f, NavMesh.AllAreas))
            {
                agente.enabled = true;
                agente.Warp(hit.position);
                agente.SetDestination(hit.position);
            }

            if (!agente.pathPending && agente.remainingDistance < 0.5f)
            {
                regresando = false;
                fishPatrol.enabled = true;
            }
        }
    }

    public void DesactivarAtaque(float segundos)
    {
        tiempoProximoAtaque = Time.time + segundos;
    }

}
