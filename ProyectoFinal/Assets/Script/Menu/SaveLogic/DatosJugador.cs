using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase serializable que representa los datos guardados de una partida 
/// del jugador, incluyendo nombre, recolectables, tiempo total jugado y
/// fecha de guardado.
/// </summary>
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
    public string fecha;

}
