using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController1 : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI partesCarretilla;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowCarretillas();
    }

    public void ShowCarretillas()
    {
        partesCarretilla.text = GameManager.Instance.carretilla.ToString();
    }
}
