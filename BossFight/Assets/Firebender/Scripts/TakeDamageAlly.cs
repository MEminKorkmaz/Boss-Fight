using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageAlly : MonoBehaviour
{
    public BenderGameManager benderGameManager;
    public BenderHealthBar benderHealthBar;
    public Animator anim;
    public int MaxHealth;
    public int CurrentHealth;

    public float InvincibleTime;

    private bool CanTakeDamage = true;



    void Awake()
    {
        anim = GetComponent<Animator>();

        benderGameManager = GameObject.FindWithTag("GameManager").GetComponent<BenderGameManager>();
    }


    void Start()
    {
        CurrentHealth = MaxHealth;

        benderHealthBar.SetMaxHealth(MaxHealth);
    }

    public void TakeDamage(int Damage)
    {
        if(!CanTakeDamage) return;
        
        CurrentHealth -= Damage;
        
        benderHealthBar.SetHealth(CurrentHealth);

        if(CurrentHealth <= 0)
        {
            Destroy(gameObject);
            benderGameManager.isGameOver = true;
        }

        CanTakeDamage = false;
        Invoke(nameof(CanTakeDamageFunc) , InvincibleTime);
    }

    void CanTakeDamageFunc()
    {
        CanTakeDamage = true;
    }
}
