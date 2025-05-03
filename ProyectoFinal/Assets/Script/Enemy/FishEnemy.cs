using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEnemy : MonoBehaviour
{

    public float velocidadNadar = 2.5f;
    public float velocidadAtaque = 5f;
    public float distanciaDeteccion = 20f;
    public float tiempoEspera = 4f;
    private float tiempoProximoAtaque = 0f;

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

        if (distancia <= distanciaDeteccion && Time.time > tiempoProximoAtaque)
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
            transform.position = Vector3.MoveTowards(transform.position, objPatrulla, velocidadNadar * Time.deltaTime);
            if(Vector3.Distance(transform.position, objPatrulla) < 0.5f)
            {
                NuevoDestino();
            }
        }

        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, 68.2f, 70.1f); // ajusta los valores según tu agua
        transform.position = pos;

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
                rb.AddTorque(Vector3.up * 0.01f, ForceMode.Impulse);
            }

            BoatHealth salud = other.GetComponent<BoatHealth>();
            if (salud != null)
            {
                salud.RecibirDaño(10f); // Puedes ajustar la cantidad de daño
            }

            ataque = false;
            tiempoProximoAtaque = Time.time + tiempoEspera;

            // Reposicionar pez para evitar spam de choques
            NuevoDestino();
        }
    }

}
