using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcebenderWallShootProjectile : MonoBehaviour
{
    [HideInInspector]
    public Shaker shaker;

    [HideInInspector]
    public Vector2 TargetPosition;

    public Rigidbody2D rb;

    public GameObject HitFxPrefab;

    public float Speed;
    public float RotateSpeed;

    public GameObject GroundParticlePrefab;

    public float AreaRadius;

    public int Damage;



    void Awake()
    {
        shaker = GameObject.FindWithTag("MainCamera").GetComponent<Shaker>();
    }


    void Start()
    {
        Destroy(gameObject , 15f);
    }


    void FixedUpdate()
    {
        Move();
    }

    
    void Move()
    {
        Vector2 dir = TargetPosition - rb.position;
        dir.Normalize();
        float rotateAmount = Vector3.Cross(dir , transform.up).z;
        rb.angularVelocity = -rotateAmount * RotateSpeed;
        rb.velocity = transform.up * Speed * Time.fixedDeltaTime;
    }

    bool isCollided;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Obstacle") || col.gameObject.CompareTag("FirebenderObstacle"))
        {
            if(isCollided) return;
            isCollided = true; // Sometimes it hits side and top ground in before being able to destroyed

            GameObject go = Instantiate(HitFxPrefab , transform.position , Quaternion.identity);
            Destroy(go , 2f);

            GameObject go2 = transform.GetChild(0).gameObject;
            go2.GetComponent<ParticleSystem>().Stop(true);
            go2.transform.parent = null;
            go2.transform.localScale = new Vector3(1f , 1f , 0f);
            Destroy(go2 , 6f);

            Destroy(gameObject);

            if(col.gameObject.CompareTag("Obstacle"))
            {
                GameObject TempGroundParticle = Instantiate(GroundParticlePrefab,
                transform.position,
                Quaternion.identity);
            }

            Collider2D[] AreaCheck = Physics2D.OverlapCircleAll(transform.position , AreaRadius);

            foreach (Collider2D collider in AreaCheck)
            {
                if(collider.gameObject.CompareTag("Firebender"))
                {
                    collider.gameObject.GetComponent<TakeDamageAlly>().TakeDamage(Damage);
                }
            }

            shaker.shakeDuration = 0.125f;
            shaker.shakeAmount = 0.75f;
        }
    }
}
