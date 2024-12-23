using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    public void Retry(){
        SceneManager.LoadScene("Level01");
    }

    public void Quit(){
        SceneManager.LoadScene("MainMenu");
    }
}
