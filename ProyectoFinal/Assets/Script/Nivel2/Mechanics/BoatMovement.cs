using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    /// <summary>
    /// Velocidad para mover el bote hacia adelante o atrás.
    /// </summary>
    public float velocidad = 600f;

    /// <summary>
    /// Velocidad para girar el bote (en grados por segundo).
    /// </summary>
    public float giroVelocidad = 50f;
    private Rigidbody rb;
    private bool puedeMoverse = true;

    private RigidbodyConstraints restriccionesOriginales;
    // Start is called before the first frame update

    /// <summary>
    /// Inicializa el Rigidbody, ajusta el centro de masa 
    /// y guarda las restricciones originales.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
        restriccionesOriginales = rb.constraints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Aplica la fuerza y rotación al bote en cada 
    /// FixedUpdate si puede moverse.
    /// </summary>
    private void FixedUpdate()
    {
        if (!puedeMoverse) return;

        // Leer la entrada de movimiento (adelante/atrás) y giro (izquierda/derecha)
        float adelante = Input.GetAxis("Vertical");
        float giro = Input.GetAxis("Horizontal");

        
        Vector3 direccionMovimiento = transform.forward * adelante;

        
        Vector3 fuerza = direccionMovimiento * velocidad;

        
        rb.AddForce(fuerza, ForceMode.Force);

        
        Quaternion rotacion = Quaternion.Euler(0f, giro * giroVelocidad * Time.fixedDeltaTime, 0f);

        
        rb.MoveRotation(rb.rotation * rotacion);
    }


    /// <summary>
    /// Activa o desactiva el movimiento del bote.
    /// </summary>
    /// <param name="estado">true para activar el movimiento, false para detenerlo.</param>
    /// <example>
    /// <code>
    /// boatMovement.ActivarMovimiento(false); // Detiene el bote
    /// boatMovement.ActivarMovimiento(true);  // Lo activa
    /// </code>
    /// </example>
    public void ActivarMovimiento(bool estado)
    {
        puedeMoverse = estado;
        if (!estado)
        {
            // Detener el movimiento completamente
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    /// <summary>
    /// Congela la inclinación del bote para evitar que se 
    /// vuelque o se incline en X y Z.
    /// </summary>

    public void CongelarInclinacion()
    {
        rb.constraints = (restriccionesOriginales | RigidbodyConstraints.FreezeRotationZ |
            RigidbodyConstraints.FreezeRotationX & ~RigidbodyConstraints.FreezeRotationY);
    }

    /// <summary>
    /// Restaura las restricciones originales del Rigidbody.
    /// </summary>
    public void RestaurarInclinacion()
    {
        rb.constraints = restriccionesOriginales;
    }
}
