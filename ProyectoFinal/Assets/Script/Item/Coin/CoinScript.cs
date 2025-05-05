using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public float velocidadRotacion = 50f; // Velocidad de rotación en grados por segundo
    public Vector3 ejeRotacion = Vector3.up; // Eje de rotación (por defecto, el eje Y)
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(ejeRotacion, velocidadRotacion * Time.deltaTime);
    }
}
