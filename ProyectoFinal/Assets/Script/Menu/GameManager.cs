using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //Recolectables
    public int Coper = 0; //10 puntos
    public int Gold = 0; //50 puntos
    public int Silver = 0; //100 puntos

    public string playerName;
    public int totalSharks;
    public float totalTime;


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
