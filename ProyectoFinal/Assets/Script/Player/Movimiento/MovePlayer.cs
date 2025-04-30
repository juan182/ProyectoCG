using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private CharacterController conn;

    //Movimiento
    float speed = 5;
    float horizontal;
    float vertical;

    //Rotacion
    Vector3 moveDirection;
    float rotationSpeed = 360;
    Quaternion toRotate;
    float magnitud;

    // Start is called before the first frame update
    void Start()
    {
        conn = GetComponent<CharacterController>();
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

        conn.SimpleMove(moveDirection * magnitud * speed);

        if(moveDirection != Vector3.zero)
        {
            toRotate = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
        }
    }
}
