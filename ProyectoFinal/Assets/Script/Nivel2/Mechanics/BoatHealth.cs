using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoatHealth : MonoBehaviour
{

    public float saludMaxima = 100f;
    public float saludActual;
    public float velocidadHundimiento = 0.1f;
    private bool hundiendose = false;
    private float profundidadMaxima = 66.4f;

    public float tiempoGO = 3f;
    public float tiempoInclinado = 0f;
    // Start is called before the first frame update
    void Start()
    {
        saludActual = saludMaxima;
        Debug.Log("Inicio del juego");
    }

    // Update is called once per frame
    void Update()
    {
        if (saludActual < saludMaxima)
        {
            
            float porcentajeDaño = 1f - (saludActual / saludMaxima); // 0 a 1
            float profundidadObjetivo = Mathf.Lerp(0f, profundidadMaxima, porcentajeDaño);

            
            Vector3 pos = transform.position;
            pos.y = Mathf.Lerp(pos.y, profundidadObjetivo, velocidadHundimiento * Time.deltaTime);
            transform.position = pos;
        }

        float inclinacion = Vector3.Angle(transform.up, Vector3.up);
        if (inclinacion > 45f)
        {
            tiempoInclinado += Time.deltaTime;

            if (tiempoInclinado >= tiempoGO)
            {
                Debug.Log("Canoa se volcó. Game Over.");
                StartCoroutine(ReiniciarEscena(1.5f));
            }
            else
            {
                tiempoInclinado = 0f;
            }
            
        }
    }

    public void RecibirDaño(float cantidad)
    {

        Debug.Log("Entró al método RecibirDaño");
        saludActual -= cantidad;
        Debug.Log("Canoa recibió daño. Salud actual: " + saludActual);

        if (saludActual <= 50f && !hundiendose)
        {
            hundiendose = true;
            Debug.Log("Entró en modo hundiéndose");
        }

        if (saludActual <= 0f)
        {
            saludActual = 0f;
            Debug.Log("Salud llego a 0, reiniciando escena en 1,5 segundos");
            StartCoroutine(ReiniciarEscena(1.5f)); // espera 3 segundos antes de reiniciar
        }
        
    }

    public void IniciarReinicio(float delay)
    {
        StartCoroutine(ReiniciarEscena(delay));
    }


    private System.Collections.IEnumerator ReiniciarEscena(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
