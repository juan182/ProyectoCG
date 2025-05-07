using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Nivel3MovePlayer : MonoBehaviour
{
    private CharacterController conn;

    //Vida
    public int health = 5;

    //Movimiento
    float speed = 5;
    float horizontal;
    float vertical;

    //Rotacion
    Vector3 moveDirection;
    float rotationSpeed = 360;
    Quaternion toRotate;
    float magnitud;

    //Animacion
    private Animator anim;

    //Posicion
    private Vector3 initialPosition;

    //Salto
    float jumpSpeed = 10;
    float ySpeed;
    Vector3 vel;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        //Componente
        conn = GetComponent<CharacterController>();
        anim = transform.GetChild(0).GetComponent<Animator>();

        //Registra posicion de inicio
        initialPosition = transform.position;
        Debug.Log("Posición inicial registrada: " + initialPosition);
    }

    // Update is called once per frame
    void Update()
    {
        //Toma el valor de Horizontal
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical")*-1;

        moveDirection = new Vector3(vertical, 0, horizontal);
        moveDirection.Normalize();

        //Magnitud
        magnitud = moveDirection.magnitude;
        magnitud = Mathf.Clamp01(magnitud);
        anim.SetFloat("Speed", magnitud);
        Debug.Log("Speed: " + magnitud);

        conn.SimpleMove(moveDirection * magnitud * speed);

        ySpeed += Physics.gravity.y * Time.deltaTime;
        
        vel = moveDirection * magnitud;
        vel.y = ySpeed;

        conn.Move(vel * Time.deltaTime);

        if (conn.isGrounded)
        {
            ySpeed = -0.5f;
            isGrounded = true;
            anim.SetBool("IsJumping", false);
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpSpeed;
                isGrounded = false;
                anim.SetBool("IsJumping", true);
            }
        }

        if(moveDirection != Vector3.zero)
        {
            toRotate = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
        }

        //Caida
        if (transform.position.y < -0.5f)
        {
            ResetPlayerPosition();
        }
    }

    public void ResetPlayerPosition()
    {
        transform.position = initialPosition;
    }
    public float threshold;

    public void hit()
    {
        health = health - 1;

        if (health <= 0)
        {
            ResetPlayerPosition();
            health = 5;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit collision)
    {
        //Enemigo
        if (collision.gameObject.CompareTag("Enemie"))
        {
            // Lógica para manejar la colisión

            hit();
        }
        //Trampa
        if (collision.gameObject.CompareTag("Trap"))
        {
            hit();
            // Lógica para manejar la colisión
            //ResetPlayerPosition();
        }
    }
}
