using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    //Movimiento
    float speed = 5;
    float horizontal;
    float vertical;

    //Rotacion
    Vector3 moveDirection;
    float rotationSpeed = 5;
    Quaternion toRotate;
    float magnitud;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Toma el valor de Horizontal
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(horizontal, 0, vertical);
        moveDirection.Normalize();

        //Magnitud
        magnitud = moveDirection.magnitude;
        magnitud = Mathf.Clamp01(magnitud);

        transform.Translate(moveDirection * speed*Time.deltaTime);

        if(moveDirection != Vector3.zero)
        {
            toRotate = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
        }
    }
}
