using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase serializable que representa el historial de partidas de jugadores.
/// Contiene la lista de objetos DatosJugador que almacenan los datos de 
/// cada partida.
/// </summary>
[System.Serializable]
public class Historial
{
    public List<DatosJugador> partidas =new List<DatosJugador>();
}
