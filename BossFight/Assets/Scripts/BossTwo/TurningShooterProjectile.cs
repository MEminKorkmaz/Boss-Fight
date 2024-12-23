using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningShooterProjectile : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed;


    void Start(){
        Destroy(gameObject , 5f);
    }

    void FixedUpdate(){
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
