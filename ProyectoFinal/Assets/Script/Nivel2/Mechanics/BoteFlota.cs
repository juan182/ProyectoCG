using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoteFlota : MonoBehaviour
{
    public float alturaAgua = 70.1f; // Nivel del agua
    public float alturaMaxInmersion = 0.5f; // Hasta qué punto el bote puede sumergirse antes de aplicar flotación total
    public float flotacion = 10f; // Qué tan fuerte es la fuerza de flotación

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.3f, 0); // Baja el centro de masa
    }

    void FixedUpdate()
    {
        float profundidad = alturaAgua - transform.position.y;

        if (profundidad > 0)
        {
            float desplazamiento = Mathf.Clamp01(profundidad / alturaMaxInmersion);
            float fuerza = Mathf.Lerp(0, Mathf.Abs(Physics.gravity.y) * desplazamiento * flotacion, desplazamiento);
            rb.AddForce(Vector3.up * fuerza, ForceMode.Acceleration);
        }
    }
}
