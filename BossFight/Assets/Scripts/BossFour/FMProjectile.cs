﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMProjectile : MonoBehaviour
{

    public Rigidbody2D rb;

    public float speed;

    public float destroyDelay;



    void Start(){
        Destroy(gameObject , destroyDelay);
    }
    
    void FixedUpdate()
    {
        Move();
    }

    void Move(){
        rb.velocity = transform.up * speed * Time.fixedDeltaTime;
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("player")){
            Destroy(gameObject);
        }
    }
}