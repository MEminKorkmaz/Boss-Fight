using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcebenderIceBullet : MonoBehaviour
{
    public Rigidbody2D rb;

    public GameObject HitFxPrefab;
    public GameObject GroundFxPrefab;

    public float AreaRadius;

    public float Speed;

    public int Damage;

    void Start()
    {
        Destroy(gameObject , 15f);
    }


    void FixedUpdate()
    {
        if(isCollided) return;
        rb.velocity = transform.up * Speed * Time.fixedDeltaTime;
    }

    bool isCollided;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Obstacle"))
        {
            if(isCollided) return;
            isCollided = true; // Sometimes it hits side and top ground in before being able to destroyed

            rb.velocity = new Vector2(0f , 0f);

            GameObject go = Instantiate(GroundFxPrefab , transform.position + new Vector3(0f , -1f , 0f),
            Quaternion.identity);
            Destroy(go , 2f);

            GameObject go2 = transform.GetChild(0).gameObject;
            go2.GetComponent<ParticleSystem>().Stop(true);
            go2.transform.parent = null;
            go2.transform.localScale = new Vector3(1f , 1f , 0f);
            Destroy(go2 , 2f);

            Destroy(gameObject , 10f);
        }

        if(col.gameObject.CompareTag("FirebenderObstacle"))
        {
            if(isCollided) return;
            isCollided = true;
            
            rb.velocity = new Vector2(0f , 0f);

            GameObject go = Instantiate(HitFxPrefab , transform.position + new Vector3(0f , -1f , 0f),
            Quaternion.identity);
            Destroy(go , 2f);

            GameObject go2 = transform.GetChild(0).gameObject;
            go2.GetComponent<ParticleSystem>().Stop(true);
            go2.transform.parent = null;
            go2.transform.localScale = new Vector3(1f , 1f , 0f);
            Destroy(go2 , 2f);

            Destroy(gameObject);
        }

        if(col.gameObject.CompareTag("Firebender"))
        {
            if(isCollided) return;
            isCollided = true;

            col.gameObject.GetComponent<TakeDamageAlly>().TakeDamage(Damage);
            
            rb.velocity = new Vector2(0f , 0f);

            GameObject go = Instantiate(HitFxPrefab , transform.position + new Vector3(0f , 0f , 0f),
            Quaternion.identity);
            Destroy(go , 2f);

            GameObject go2 = transform.GetChild(0).gameObject;
            go2.GetComponent<ParticleSystem>().Stop(true);
            go2.transform.parent = null;
            go2.transform.localScale = new Vector3(1f , 1f , 0f);
            Destroy(go2 , 2f);

            Destroy(gameObject);
        }
    }
}
