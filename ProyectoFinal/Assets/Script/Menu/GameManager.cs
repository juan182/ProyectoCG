using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Controlador principal del juego. Administra los recolectables, la salud,
/// el nombre del jugador y los tiempos por escena. Persiste entre escenas.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //Recolectables

    /// <summary>Recolectables de la Escena 1.</summary>
    public int carretilla = 0;

    /// <summary>Recolectables de la Escena 2.</summary>
    public int shark = 0;

    /// <summary>Recolectables de la Escena 3: cobre, oro y plata.</summary>
    public int copper = 0; //10 puntos
    public int gold = 0; //50 puntos
    public int silver = 0; //100 puntos

    /// <summary>Salud del jugador (máximo 5).</summary>
    public int health = 5;

    /// <summary>Diccionario que almacena el tiempo acumulado por escena.</summary>
    public Dictionary<string, float> tiempoEscenas = new Dictionary<string, float>();

    #region Jugador
    public string nombreJugador;
    #endregion


    /// <summary>
    /// Asigna esta instancia como Singleton y persiste entre escenas.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);


        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>Agrega valor a carretillas.</summary>
    public void sumCarretilla(int value)
    {
        carretilla += value;
    }

    /// <summary>Agrega valor a tiburones.</summary>
    public void sumShark(int value)
    {
        shark += value;
    }

    /// <summary>Agrega valor a monedas de cobre.</summary>
    public void sumCopperCoin(int value)
    {
        copper += value;
    }

    /// <summary>Agrega valor a monedas de plata.</summary>
    public void sumSilverCoin(int value)
    {
        silver += value;
    }

    /// <summary>Agrega valor a monedas de oro.</summary>
    public void sumGoldenCoin(int value)
    {
        gold += value;
    }

    /// <summary>Aumenta la salud del jugador, sin pasar el máximo de 5.</summary>
    public void sumHealth(int value)
    {
        health += value;
        health = Mathf.Min(health, 5);
    }

    /// <summary>Resetea la cantidad de tiburones recolectados.</summary>
    public void ResetShark()
    {
        shark = 0;
    }

    /// <summary>Resetea todos los recolectables.</summary>
    public void resetValue()
    {
        //Escena 1
        carretilla = 0;

        //Escena 2
        shark = 0;

        //Escena 3
        copper = 0;
        gold = 0; 
        silver = 0;

        
    }

    public int  Carretilla { get => carretilla; set => carretilla = value; }
    public int Shark { get => shark; set => shark = value; }
    public int Copper { get => copper; set => copper = value; }
    public int Silver { get => silver; set => silver = value; }
    public int Gold { get => gold; set => gold = value; }
    public int Health { get => health; set => health = value; }


    /// <summary>
    /// Guarda el tiempo jugado en una escena específica. Si ya existía, lo suma.
    /// </summary>
    /// <param name="nombreEscena">Nombre de la escena.</param>
    /// <param name="tiempo">Tiempo jugado en esa escena.</param>
    public void GuardarTiemposEscenas(string nombreEscena, float tiempo)
    {
        if (tiempoEscenas.ContainsKey(nombreEscena))
        {
            tiempoEscenas[nombreEscena] += tiempo;
        }
        else
        {
            tiempoEscenas[nombreEscena] = tiempo;
        }
    }

    /// <summary>
    /// Suma y devuelve el tiempo total jugado entre todas las escenas.
    /// </summary>
    /// <returns>Tiempo total jugado.</returns>
    public float ObtenerTiempoTotal()
    {
        float total = 0f;
        foreach(var tiempo in tiempoEscenas.Values)
        {
            total += tiempo;
        }
        return total;
    }

}
