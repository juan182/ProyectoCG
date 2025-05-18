using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController3 : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI txtCopperCoin;

    [SerializeField]
    private TextMeshProUGUI txtSilverCoin;

    [SerializeField]
    private TextMeshProUGUI txtGoldCoin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowCopperCoin();
        ShowSilveCoin();
        ShowGoldCoin();
    }

    public void ShowCopperCoin()
    {
        txtCopperCoin.text = GameManager.Instance.copper.ToString();
    }
    public void ShowSilveCoin()
    {
        txtCopperCoin.text = GameManager.Instance.copper.ToString();
    }
    public void ShowGoldCoin()
    {
        txtCopperCoin.text = GameManager.Instance.copper.ToString();
    }

}
