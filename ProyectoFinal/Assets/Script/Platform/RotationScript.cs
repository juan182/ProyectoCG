using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public float velocidadRotacion = 50f; // Velocidad de rotaci�n en grados por segundo
    public Vector3 ejeRotacion = Vector3.up; // Eje de rotaci�n (por defecto, el eje Y)

    void Update()
    {
        // Aplica la rotaci�n
        transform.Rotate(ejeRotacion, velocidadRotacion * Time.deltaTime);
    }
}
