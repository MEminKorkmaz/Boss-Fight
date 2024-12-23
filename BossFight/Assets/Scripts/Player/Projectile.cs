using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Projectile : MonoBehaviour
{
    public SpriteRenderer ProjectileSR;

    public Sprite CritSprite;

    public Rigidbody2D rb;

    public GameObject ExplosionPrefab;

    public Transform Target;

    public float speed;

    public float rotatingSpeed;
    
    public int damage;

    public int critChance;

    public float critMultiplier;

    private bool isCrit;

    private bool isHomingBullet;

    public PlayerManager playerManager;


    void Awake(){
        playerManager = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>();
    }


    void Start(){

        ProjectileSR = transform.GetChild(5).GetComponent<SpriteRenderer>();

        critChance = playerManager.critChance;
        critMultiplier = playerManager.critMultiplier;

        damage = playerManager.damage + playerManager.bonusDamage;

        isHomingBullet = playerManager.isHomingBullet;
        
        int rnd = Random.Range(0 , 100);

        if(rnd < critChance){
            damage = (int)(damage * critMultiplier);
            isCrit = true;
            ProjectileSR.sprite = CritSprite;
        }

        Destroy(gameObject , 3f);
    }

    void Update()
    {
        isHomingBullet = playerManager.isHomingBullet;
    }
    void FixedUpdate()
    {
        if(isHomingBullet){
            Rotation();
        }
        else{
        Move();
        }
    }

    void Move(){
        rb.velocity = transform.up * speed * Time.fixedDeltaTime;
    }

    void Rotation(){
        Target = getClosestEnemy();
        if(Target == null) return;
        
        Vector2 dir = (Vector2)Target.position - rb.position;
        dir.Normalize();
        float rotateAmount = Vector3.Cross(dir , transform.up).z;
        rb.angularVelocity = -rotateAmount * rotatingSpeed;
        rb.velocity = transform.up * speed * Time.fixedDeltaTime;
        }


        private GameObject[] enemies;
        public Transform getClosestEnemy(){
        enemies = FindObjectsOfType(typeof(GameObject))
        .Cast<GameObject>()
        .Where(enemies => enemies.tag == "Enemy" || enemies.tag == "Boss")
        .ToArray();
        float closestDistance = Mathf.Infinity;
        Transform trans = null;

        foreach(GameObject go in enemies){
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position , go.transform.position);
            if(currentDistance < closestDistance){
                closestDistance = currentDistance;
                trans = go.transform;
            }
        }
        return trans;
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("Boss")){
        col.GetComponent<BossHealth>().TakeDamage(damage);
        Explode();
        }

        if(col.gameObject.CompareTag("Enemy")){
        col.GetComponent<EnemyHealth>().TakeDamage(damage);
        Explode();
        }
    }

    void Explode(){
        Destroy(gameObject);

        if(isCrit){
            GameObject go2 = Instantiate(ExplosionPrefab , transform.position , transform.rotation);
            go2.GetComponent<SpriteRenderer>().color = Color.red;
            Destroy(go2 , 0.15f);
        }
        else{
        GameObject go = Instantiate(ExplosionPrefab , transform.position , transform.rotation);
        Destroy(go , 0.15f);
        }
    }
}
