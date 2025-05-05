using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //Cargar scene 
    public void LoadScene(string nameScene)
    {
        if (Timer.Instance != null)
        {
            Timer.Instance.TimerStart();
        }
        SceneManager.LoadScene(nameScene);

    }

    public void CloseGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}