using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //Recolectables
    //private int Coper = 0; //10 puntos
    //private int Gold = 0; //50 puntos
    //private int Silver = 0; //100 puntos

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
}
