using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Simula la flotación de un bote en un nivel de agua fijo.
/// Aplica una fuerza hacia arriba proporcional a qué tanto 
/// está sumergido el bote.
/// </summary>
public class BoteFlota : MonoBehaviour
{
    /// <summary>
    /// Nivel del agua en coordenadas Y del mundo.
    /// </summary>
    [SerializeField]
    private float alturaAgua = 69.22f;

    /// <summary>
    /// Profundidad máxima que puede sumergirse el bote antes de aplicar toda 
    /// la fuerza de flotación.
    /// </summary>
    [SerializeField]
    private float alturaMaxInmersion = 0.5f;

    /// <summary>
    /// Factor que determina cuán fuerte es la flotación del bote.
    /// </summary>
    [SerializeField]
    private float flotacion = 15f; 

    private Rigidbody rb;

    /// <summary>
    /// Inicializa el Rigidbody y ajusta el centro de masa para mayor 
    /// estabilidad.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.3f, 0); // Baja el centro de masa para que sea más estable
    }

    /// <summary>
    /// Aplica fuerzas físicas de flotación en cada actualización de física.
    /// </summary>
    void FixedUpdate()
    {
        float profundidad = alturaAgua - transform.position.y; // Qué tan hundido está el bote

        if (profundidad > 0)
        {
            // Calcula cuánto se está hundiendo (0 a 1)
            float desplazamiento = Mathf.Clamp01(profundidad / alturaMaxInmersion);

            // Calcula la fuerza de empuje que depende de qué tanto está sumergido
            float fuerza = Mathf.Abs(Physics.gravity.y) * desplazamiento * desplazamiento * flotacion;

            // Aplica la fuerza de empuje
            rb.AddForce(Vector3.up * fuerza, ForceMode.Acceleration);
        }
    }
}
