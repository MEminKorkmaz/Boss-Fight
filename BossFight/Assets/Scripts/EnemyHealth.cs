using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;

    private int health;



    void Start(){
        health = maxHealth;
    }
    
    void Update(){
        HealthChecker();
    }

    void HealthChecker(){
        if(health <= 0){
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage){
        health -= damage;
    }
}
