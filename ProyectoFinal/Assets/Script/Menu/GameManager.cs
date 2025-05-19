using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //Recolectables

    //Escena 1
    public int carretilla = 0;

    //Escena 2
    public int shark = 0;

    //Escena 3
    public int copper = 0; //10 puntos
    public int gold = 0; //50 puntos
    public int silver = 0; //100 puntos

    //private int health = 5;

    // Diccionario para guardar tiempo de escenas
    public Dictionary<string, float> tiempoEscenas = new Dictionary<string, float>();



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
     // Escena 1
    public void sumCarretilla(int value)
    {
        carretilla += value;
    }

    // Escena 2
    public void sumShark(int value)
    {
        shark += value;
    }

    // Escena 3
    public void sumCopperCoin(int value)
    {
        copper += value;
    }
    public void sumSilverCoin(int value)
    {
        silver += value;
    }
    public void sumGoldenCoin(int value)
    {
        gold += value;
    }

    public void ResetShark()
    {
        shark = 0;
    }

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
