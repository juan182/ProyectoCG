using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoteFlota : MonoBehaviour
{
    [SerializeField]
    private float alturaAgua = 69.22f;
    [SerializeField]
    private float alturaMaxInmersion = 0.5f;
    [SerializeField]
    private float flotacion = 15f; 

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
            float fuerza = Mathf.Abs(Physics.gravity.y) * desplazamiento * desplazamiento * flotacion;
            rb.AddForce(Vector3.up * fuerza, ForceMode.Acceleration);
        }
    }
}
