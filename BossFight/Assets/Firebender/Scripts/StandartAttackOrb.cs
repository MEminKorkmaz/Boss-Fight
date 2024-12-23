using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartAttackOrb : MonoBehaviour
{
    public Rigidbody2D rb;

    public GameObject HitFxPrefab;

    public float Speed;

    public int Damage;

    void Start()
    {
        Destroy(gameObject , 15f);
    }


    void FixedUpdate()
    {
        rb.velocity = transform.up * Speed * Time.fixedDeltaTime;
    }

    bool isCollided;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Obstacle"))
        {
            if(isCollided) return;
            isCollided = true; // Sometimes it hits side and top ground in before being able to destroyed

            GameObject go = Instantiate(HitFxPrefab , transform.position , Quaternion.identity);
            Destroy(go , 2f);

            GameObject go2 = transform.GetChild(0).gameObject;
            go2.GetComponent<ParticleSystem>().Stop(true);
            go2.transform.parent = null;
            go2.transform.localScale = new Vector3(1f , 1f , 0f);
            Destroy(go2 , 1f);

            Destroy(gameObject);
        }

        if(col.gameObject.CompareTag("Icebender"))
        {
            if(isCollided) return;
            isCollided = true; // Sometimes it hits side and top ground in before being able to destroyed
            
            col.gameObject.GetComponent<TakeDamageEnemy>().TakeDamage(Damage);
            GameObject go = Instantiate(HitFxPrefab , transform.position , Quaternion.identity);
            Destroy(go , 2f);

            GameObject go2 = transform.GetChild(0).gameObject;
            go2.GetComponent<ParticleSystem>().Stop(true);
            go2.transform.parent = null;
            go2.transform.localScale = new Vector3(1f , 1f , 0f);
            Destroy(go2 , 1f);

            Destroy(gameObject);
        }
    }
}
