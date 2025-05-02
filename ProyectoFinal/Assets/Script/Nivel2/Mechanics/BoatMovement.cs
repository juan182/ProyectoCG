using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public float velocidad = 600f;
    public float giroVelocidad = 50f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0); // más estable
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Leer la entrada de movimiento (adelante/atrás) y giro (izquierda/derecha)
        float adelante = Input.GetAxis("Vertical");
        float giro = Input.GetAxis("Horizontal");

        
        Vector3 direccionMovimiento = transform.forward * adelante;

        
        Vector3 fuerza = direccionMovimiento * velocidad;

        
        rb.AddForce(fuerza, ForceMode.Force);

        
        Quaternion rotacion = Quaternion.Euler(0f, giro * giroVelocidad * Time.fixedDeltaTime, 0f);

        
        rb.MoveRotation(rb.rotation * rotacion);
    }
}
