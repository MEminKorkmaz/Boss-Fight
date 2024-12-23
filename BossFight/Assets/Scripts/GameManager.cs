using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager SharedInstance;

    [Header("UI PART")]
    public Text LivesTxt;
    
    public Text TimerTxt;
    
    public Text LevelTxt;

    private int life;

    private float startTime;

    [Header("GAME STATUS")]
    public bool isGameOver;
    public bool isLevelEnded;

    public int level;

    /*[Header("Player Stats")]
    public float fireRate;

    public int critChance;

    public float critMultiplier;

    public int damage;

    public int bonusDamage;

    public int Lives;*/

    [Header("Others")]
    public PlayerGeneral playerGeneral;

    public PlayerManager playerManager;
    
    public int maxLevels;

    private bool isPaused;

    //public Text PausedTxt;
    public GameObject PausePanel;

    public GameObject UpgradePanel;

    public GameObject LosePanel;

    public Text MaxLevelTxt;

    public Text BestTxt;

    public GameObject QuitBtn;




    void Awake(){
        SharedInstance = this;
        
        playerGeneral = GameObject.FindWithTag("player").GetComponent<PlayerGeneral>();

        playerManager = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>();
    }

    
    void Start()
    {
        UpgradePanel.SetActive(false);

        life = playerGeneral.Lives;

        startTime = Time.time;

        isGameOver = false;

        isLevelEnded = false;

        CameraShake.SharedInstance.LevelStart();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(isGameOver || isLevelEnded) return;
        // UI PART
        LivesFunc();
        TimerFunc();
        LevelFunc();

        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                isPaused = !isPaused;
                PausePanel.SetActive(false);
                //PausedTxt.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }

            else if(!isPaused){
                isPaused = !isPaused;
                PausePanel.SetActive(true);
                //PausedTxt.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    void LivesFunc(){
        if(playerGeneral.Lives == 0)
        LivesTxt.text = ("Lives\n");

        else if(playerGeneral.Lives == 1)
        LivesTxt.text = ("Lives\n") + ("X");

        else if(playerGeneral.Lives == 2)
        LivesTxt.text = ("Lives\n") + ("XX");

        else if(playerGeneral.Lives == 3)
        LivesTxt.text = ("Lives\n") + ("XXX");

        else if(playerGeneral.Lives == 4)
        LivesTxt.text = ("Lives\n") + ("XXXX");

        else if(playerGeneral.Lives == 5)
        LivesTxt.text = ("Lives\n") + ("XXXXX");

        else if(playerGeneral.Lives == 6)
        LivesTxt.text = ("Lives\n") + ("XXXXXX");
    }

    void TimerFunc(){
        //playerManager.t = Time.time - startTime;
        
        string min = ((int) playerManager.t / 60).ToString();
        string sec = (playerManager.t % 60).ToString("0");

        TimerTxt.text = min + " : " + sec;
    }

    void LevelFunc(){
        LevelTxt.text = ("Level\n") + level.ToString() + " / " + maxLevels;
    }

    public void UpgradePanelFunc(){
        if(level == maxLevels) return;
        
        UpgradePanel.SetActive(true);
    }

    public void GameOver(){
        isGameOver = true;
        playerManager.ClearStats();
        LosePanel.SetActive(true);
    }

    private string tempMin;
    private string tempSec;

    public void LevelEnded(){
        isLevelEnded = true;
        if(level == maxLevels){
            MaxLevelTxt.gameObject.SetActive(true);
            BestTxt.gameObject.SetActive(true);
            QuitBtn.SetActive(true);
            
            float tempT = playerManager.t;
            float t = PlayerPrefs.GetFloat("time" , 1000f);
            if(tempT < t){
                PlayerPrefs.SetFloat("time" , tempT);
                tempMin = ((int) tempT / 60).ToString();
                tempSec = (tempT % 60).ToString("0");
            }

            else{
            tempMin = ((int) t / 60).ToString();
            tempSec = (t % 60).ToString("0");
            }
            BestTxt.text = "BEST   " + tempMin + " : " + tempSec;
        }
    }
}
