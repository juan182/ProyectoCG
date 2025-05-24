using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// Controla a un enemigo que sigue continuamente al jugador usando NavMeshAgent.
/// </summary>
/// <remarks>
/// Requiere que el objeto tenga un componente NavMeshAgent y que se le asigne 
/// un Transform del jugador.
/// </remarks>
/// <example>
/// Este script se usa en enemigos para que persigan al jugador en tiempo real:
/// <code>
/// public class FollowGrunt : MonoBehaviour
/// {
///     public NavMeshAgent enemy;
///     public Transform player;
/// 
///     void Update()
///     {
///         enemy.SetDestination(player.position);
///     }
/// }
/// </code>
/// </example>
public class FollowGrunt : MonoBehaviour
{
    /// <summary>
    /// Componente NavMeshAgent del enemigo para navegación.
    /// </summary>
    public NavMeshAgent enemy;

    /// <summary>
    /// Transform del jugador a seguir.
    /// </summary>
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    /// <summary>
    /// Actualiza la posición de destino del NavMeshAgent para seguir al jugador.
    /// </summary>
    /// <example>
    /// En cada frame, el enemigo actualiza su destino hacia la posición actual 
    /// del jugador.
    /// </example>
    void Update()
    {
        enemy.SetDestination(player.position);
    }
}
