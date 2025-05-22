using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverCoin : MonoBehaviour
{
    private int point = 100;
    public AudioClip audioSilver;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.sumSilverCoin(point);
            if (audioSilver != null)
            {
                AudioSource.PlayClipAtPoint(audioSilver, Camera.main.transform.position);
            }
        }
        Destroy(gameObject);
    }

}
