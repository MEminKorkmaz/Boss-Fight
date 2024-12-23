using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrow : MonoBehaviour
{
    public Rigidbody2D rb;

    //public GameObject ExplosionFxPrefab;

    public float speed;

    private bool isAction;
    
    public float actionTimer;



    void Start(){
        Invoke(nameof(Explode) , 10f);

        Invoke(nameof(Action) , actionTimer);
    }

    void FixedUpdate(){
        Move();
    }

    void Move(){
        if(!isAction) return;
        rb.velocity = transform.up * speed * Time.fixedDeltaTime;
    }

    void Action(){
        isAction = true;
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("player")){
            col.GetComponent<PlayerGeneral>().TakeDamage();
            Explode();
        }
    }

    void Explode(){
        /*GameObject go = Instantiate(ExplosionFxPrefab , transform.position , transform.rotation);
        Destroy(go , 1f);*/
        Destroy(gameObject);
    }

}
