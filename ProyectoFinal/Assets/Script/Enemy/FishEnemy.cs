using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEnemy : MonoBehaviour
{

    public float velocidadNadar = 1.5f;
    public float velocidadAtaque = 5f;
    public float distanciaDeteccion = 5f;
    public float tiempoEspera = 3f;
    public float ultimoAtaque = 0f;

    private Vector3 objPatrulla;
    private bool ataque = false;
    private Transform canoa;

    // Start is called before the first frame update
    void Start()
    {
        canoa = GameObject.FindGameObjectWithTag("Player").transform;
        NuevoDestino();
    }

    // Update is called once per frame
    void Update()
    {
        float distancia = Vector3.Distance(transform.position, canoa.position);

        if (distancia <= distanciaDeteccion && Time.time > ultimoAtaque)
        {
            ataque = true;
        }
        if (ataque)
        {
            transform.position = Vector3.MoveTowards(transform.position, canoa.position, velocidadAtaque * Time.deltaTime);
        }
        else
        {
            //Patrulla
            transform.position = Vector3.MoveTowards(transform.position, objPatrulla, velocidadAtaque * Time.deltaTime);
            if(Vector3.Distance(transform.position, objPatrulla) < 0.5f)
            {
                NuevoDestino();
            }
        }
    }

    void NuevoDestino()
    {
        float rango = 10f;
        objPatrulla = new Vector3(
            transform.position.x + Random.Range(-rango, rango),
            transform.position.y + Random.Range(-1f, 1f),
            transform.position.z + Random.Range(-rango, rango)
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Aplicar una leve rotación a la canoa
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddTorque(Vector3.up * 5f, ForceMode.Impulse);
            }

            ataque = false;
            ultimoAtaque = Time.time + tiempoEspera;

            // Reposicionar pez para evitar spam de choques
            NuevoDestino();
        }
    }

}
