using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    public void Quit(){
        SceneManager.LoadScene("MainMenu");
    }
}
