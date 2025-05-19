using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DatosJugador 
{
    public string nombreJugador;
    public int carretilla;
    public int goldCoin;
    public int silverCoin;
    public int copperCoin;
    public int shark;
    public float tiempoTotal;
    public List<TiempoEscena> tiemposPorEscena;

}
