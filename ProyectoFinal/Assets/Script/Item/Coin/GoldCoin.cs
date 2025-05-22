using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    private int point = 50;

    public AudioClip audioGold;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.sumGoldenCoin(point);
            if (audioGold != null)
            {
                AudioSource.PlayClipAtPoint(audioGold, Camera.main.transform.position);
            }
        }
        Destroy(gameObject);
    }
}
