using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager SharedInstance;

    //public GameManager gameManager;

    public float fireRate;

    public int critChance;

    public float critMultiplier;

    public int damage;

    public int bonusDamage;

    public int lives;

    public bool isDoubleBullet;
    public bool isTripleBullet;
    public bool isHomingBullet;

    private float startTime;
    public float t;

    public bool InvincibleMode;


    void Awake(){
        if(SharedInstance == null)
        SharedInstance = this;

        else{
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void ClearStats(){
        critChance = 0;
        bonusDamage = 0;
        lives = 3;
        isDoubleBullet = false;
        isTripleBullet = false;
        isHomingBullet = false;
        t = 0f;
    }

    void Start(){
        //gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        startTime = Time.time;
    }

    void Update(){
        //if(gameManager.isGameOver || gameManager.isLevelEnded) return;
        if(GameManager.SharedInstance.isGameOver || GameManager.SharedInstance.isLevelEnded) return;

        t += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if(Input.GetKeyDown(KeyCode.Keypad4))
        {
            damage += 50;
        }

        if(Input.GetKeyDown(KeyCode.Keypad5))
        {
            damage -= 50;
        }

        if(Input.GetKeyDown(KeyCode.Keypad7))
        {
            isDoubleBullet = !isDoubleBullet;
        }

        if(Input.GetKeyDown(KeyCode.Keypad8))
        {
            isTripleBullet = !isTripleBullet;
        }

        if(Input.GetKeyDown(KeyCode.Keypad9))
        {
            isHomingBullet = !isHomingBullet;
        }

        if(Input.GetKeyDown(KeyCode.Keypad6))
        {
            critChance += 20;
            if(critChance >= 100)
                critChance = 100;
        }

        if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            critChance -= 20;
            if(critChance <= 0)
                critChance = 0;
        }
    }
}
