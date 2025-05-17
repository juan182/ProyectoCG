using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaAtaque : MonoBehaviour
{

    private SharkPatrol scriptPatrulla;


    // Start is called before the first frame update
    void Start()
    {
        scriptPatrulla = GetComponentInParent<SharkPatrol>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scriptPatrulla.Persecucion(other.transform);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scriptPatrulla.SalirPersecucion();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
