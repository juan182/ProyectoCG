using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEnemy : MonoBehaviour
{
    public float velocidadNadar = 5f;
    public float velocidadAtaque = 8f;
    public float distanciaDeteccion = 25f;
    public float tiempoEspera = 2f;
    public float alturaEmerger = 5f;

    public Escape escape;
    private Transform canoa;

    public Transform[] puntosPatrulla;
    private int indiceActual;

    private bool atacando = false;
    private bool regresando = false;
    private float tiempoProximoAtaque = 0f;

    private Vector3 posicionInicioAtaque;

    private void Start()
    {
        canoa = GameObject.FindGameObjectWithTag("Player").transform;
        NuevoDestino();
    }

    private void Update()
    {
        float distancia = Vector3.Distance(transform.position, canoa.position);

        if (!atacando && !regresando && distancia <= distanciaDeteccion && Time.time > tiempoProximoAtaque)
        {
            atacando = true;
            posicionInicioAtaque = transform.position;

            // Hacer que sobresalga del agua (opcional)
            transform.position += new Vector3(0, alturaEmerger, 0);
        }

        if (atacando)
        {
            transform.position = Vector3.MoveTowards(transform.position, canoa.position, velocidadAtaque * Time.deltaTime);

            if (Vector3.Distance(transform.position, canoa.position) < 1f)
            {
                atacando = false;
                regresando = true;
                tiempoProximoAtaque = Time.time + tiempoEspera;
                canoa.GetComponent<BoatMovement>().ActivarMovimiento(false);
                escape.InicioEscape();
            }
        }
        else if (regresando)
        {
            transform.position = Vector3.MoveTowards(transform.position, posicionInicioAtaque, velocidadNadar * Time.deltaTime);

            if (Vector3.Distance(transform.position, posicionInicioAtaque) < 0.1f)
            {
                regresando = false;
            }
        }
        else
        {
            Patrullar();
        }
    }

    private void Patrullar()
    {
        if (puntosPatrulla.Length == 0)
        {
            Debug.LogWarning("No hay puntos de patrulla asignados.");
            return;
        }

        Debug.Log("Patrullando hacia: " + puntosPatrulla[indiceActual].name);

        Transform destino = puntosPatrulla[indiceActual];
        transform.position = Vector3.MoveTowards(transform.position, destino.position, velocidadNadar * Time.deltaTime);

        if (Vector3.Distance(transform.position, destino.position) < 0.1f)
        {
            NuevoDestino();
        }
    }


    private void NuevoDestino()
    {
        if (puntosPatrulla.Length == 0) return;
        indiceActual = Random.Range(0, puntosPatrulla.Length);
    }

}
