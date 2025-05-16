using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkAttackManager : MonoBehaviour
{
    [SerializeField]
    private SharkPatrol[] sharkPatrol;
    [SerializeField]
    private GameObject[] zonaAtaque;

    private void Awake()
    {
        
        if (sharkPatrol == null || sharkPatrol.Length == 0)
            sharkPatrol = GetComponentsInChildren<SharkPatrol>();

        
        if (zonaAtaque != null)
        {
            foreach (var zona in zonaAtaque)
                if (zona != null)
                    zona.SetActive(true);
        }
    }

    public void DetenerAtaqueYPatrulla()
    {
        if (zonaAtaque != null)
        {
            foreach (var zona in zonaAtaque)
                if (zona != null)
                    zona.SetActive(false);
        }

        if (sharkPatrol != null)
        {
            foreach (var tiburon in sharkPatrol)
                if (tiburon != null)
                    tiburon.DetenerPatrulla();
        }
    }

    public void ReanudarAtaqueYPatrulla(float delay = 7f)
    {
        StartCoroutine(ReanudarDespuesDeMinijuego(delay));
    }

    private IEnumerator ReanudarDespuesDeMinijuego(float segundos)
    {
        yield return new WaitForSeconds(segundos);

        if (sharkPatrol != null)
        {
            foreach (var tiburon in sharkPatrol)
                if (tiburon != null)
                    tiburon.ReanudarPatrulla();
        }

        if (zonaAtaque != null)
        {
            foreach (var zona in zonaAtaque)
                if (zona != null)
                    zona.SetActive(true);
        }
    }
}
