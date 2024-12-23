using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageEnemy : MonoBehaviour
{
    public BenderGameManager benderGameManager;
    public BenderHealthBar benderHealthBar;
    public int MaxHealth;
    public int CurrentHealth;

    public float InvincibleTime;

    private bool CanTakeDamage;



    void Awake()
    {
        benderGameManager = GameObject.FindWithTag("GameManager").GetComponent<BenderGameManager>();
    }


    void Start()
    {
        CurrentHealth = MaxHealth;

        benderHealthBar.SetMaxHealth(MaxHealth);
    }

    public void TakeDamage(int Damage)
    {
        CurrentHealth -= Damage;

        benderHealthBar.SetHealth(CurrentHealth);

        if(CurrentHealth <= 0)
        {
            Destroy(gameObject);
            benderGameManager.isGameOver = true;
        }
    }
}
