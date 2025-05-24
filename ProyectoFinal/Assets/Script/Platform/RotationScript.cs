using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gira el objeto constantemente sobre un eje con una velocidad definida.
/// </summary>
public class RotationScript : MonoBehaviour
{
    public float velocidadRotacion = 50f; // Velocidad de rotación en grados por segundo
    public Vector3 ejeRotacion = Vector3.up; // Eje de rotación (por defecto, el eje Y)

    void Update()
    {
        // Aplica la rotación
        transform.Rotate(ejeRotacion, velocidadRotacion * Time.deltaTime);
    }
}
