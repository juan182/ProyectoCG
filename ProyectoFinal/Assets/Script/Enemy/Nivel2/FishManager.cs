using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class FishManager : MonoBehaviour
{

    public GameObject fishPrefab;
    public Transform[] puntosPatrullaTotales;
    public int cantidadTiburones = 4;
    public int puntosPorTiburon = 2;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < cantidadTiburones; i++)
        {
            int indiceSpawn = Random.Range(0, puntosPatrullaTotales.Length);
            Vector3 posicionReferencia = puntosPatrullaTotales[indiceSpawn].position;

            NavMeshHit hit;
            Vector3 posicionSpawn;

            if (NavMesh.SamplePosition(posicionReferencia, out hit, 5.0f, NavMesh.AllAreas))
            {
                posicionSpawn = hit.position;
                Debug.Log("Posición encontrada en NavMesh: " + posicionSpawn);
            }
            else
            {
                Debug.LogWarning("No se encontró una posición válida en el NavMesh cerca de: " + posicionReferencia);
                posicionSpawn = posicionReferencia;
            }

            GameObject nuevoTiburon = Instantiate(fishPrefab, posicionSpawn, Quaternion.identity);

            List<Transform> puntosAsignados = new List<Transform>();
            while (puntosAsignados.Count < puntosPorTiburon)
            {
                Transform punto = puntosPatrullaTotales[Random.Range(0, puntosPatrullaTotales.Length)];
                if (!puntosAsignados.Contains(punto))
                    puntosAsignados.Add(punto);
            }

            // 4. Asignar al script del tiburón
            FishPatrol fishPatrol = nuevoTiburon.GetComponent<FishPatrol>();
            if (fishPatrol != null)
            {
                fishPatrol.PuntosPatrulla(puntosAsignados.ToArray());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
