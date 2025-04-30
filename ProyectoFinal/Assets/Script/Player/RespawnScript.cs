using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    //Posicion
    private Vector3 initialPosition;

    void Start()
    {
        //Registra posicion de inicio
        initialPosition = transform.position;
    }

    public void ResetPlayerPosition()
    {
        transform.position = initialPosition;
    }
    public float threshold;
}
