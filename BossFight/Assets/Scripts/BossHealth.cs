using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public GameObject DeathFxPrefab;

    public HealthBar healthBar;

    public int maxHealth;

    private int currentHealth;

    public GameManager gameManager;


    void Awake(){
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }


    void Start(){
        currentHealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);
    }

    void Update(){
        HealthChecker();
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    void HealthChecker(){
        if(currentHealth <= 0){
            this.gameObject.SetActive(false);
            gameManager.LevelEnded();
            gameManager.UpgradePanelFunc();
            GameObject go = Instantiate(DeathFxPrefab , transform.position , transform.rotation);
            Destroy(go , 1.2f);
        }
    }
}
