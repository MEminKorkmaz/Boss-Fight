using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGeneral : MonoBehaviour
{
    public Animator anim;

    public int Lives;

    private bool isVulnerable;

    public PlayerManager playerManager;

    public GameManager gameManager;



    void Start(){
        playerManager = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>();

        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        isVulnerable = true;

        anim = GetComponent<Animator>();

        Lives = playerManager.lives;
    }

    void Update(){
        if(gameManager.isGameOver || gameManager.isLevelEnded) return;

        CheckHealth();

        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            Lives = 3;
        }
    }


    public void TakeDamage(){
        if(gameManager.isGameOver || gameManager.isLevelEnded) return;
        if(!isVulnerable) return;
        Lives--;
        anim.SetTrigger("PlayerHit");
        isVulnerable = false;
        Invoke(nameof(isVulnerableFunc) , 1f);
        CameraShake.SharedInstance.LittleShake();
        // Debug.Log("dead");
    }

    public void SetHealth(int health){
        Lives = health;
    }

    void isVulnerableFunc(){
        isVulnerable = true;
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("Obstacle")){
            if(playerManager.InvincibleMode) return;
            TakeDamage();
        }
    }

    void CheckHealth(){
        if(Lives <= 0){
            this.gameObject.SetActive(false);
            gameManager.GameOver();
            // Debug.Log(Lives);
        }
    }
}
