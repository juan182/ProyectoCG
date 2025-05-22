using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameObject objectToActivate;
    public int point = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.sumCarretilla(point);
            }
            Destroy(gameObject);

            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }
        }
    }
}
