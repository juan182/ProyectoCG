using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopperCoin : MonoBehaviour
{
    private int point = 10;

    public AudioClip audioCopper;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.sumCopperCoin(point);

            if (audioCopper != null)
            {
                AudioSource.PlayClipAtPoint(audioCopper, Camera.main.transform.position);
            }

            Destroy(gameObject);
        }
    }
}
