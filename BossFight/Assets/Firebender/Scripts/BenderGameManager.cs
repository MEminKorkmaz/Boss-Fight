using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BenderGameManager : MonoBehaviour
{
    public bool isGameOver;

    void Awake()
    {
        Application.targetFrameRate = 60;
        
        isGameOver = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
