using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public Rigidbody2D rb;

    public Vector3 target;

    public GameObject ExplosionPrefab;
    public GameObject TargetPrefab;
    //public GameObject RemainingFirePrefab;

    public float rotatingSpeed;
    public float speed;
    
    public float actionTime;
    private bool isAction;
    private bool canDamage;

    //public float remainingFireTimer;


    void Start(){
        if(GameObject.FindWithTag("GameManager").GetComponent<GameManager>().isGameOver){
            target = new Vector3(0.5f , -1.5f , 0f);
            return;
        }
        //target = GameObject.FindWithTag("player").transform;
        //Invoke(nameof(isActionFunc) , actionTime);
        isActionFunc();
        //Debug.Log(target);
        Invoke(nameof(Explode) , 5f);
    }


    void FixedUpdate(){
        //Action();
        Move();
        CheckForTarget();
        Rotation();
    }

    void Move(){
        //rb.velocity = transform.up * speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position , target , speed * Time.fixedDeltaTime);
    }

    void Rotation(){
        Vector3 dir = (target - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float offset = 270f;
        transform.rotation = Quaternion.Lerp(transform.rotation ,
        Quaternion.Euler(0f , 0f , (angle + offset)) , rotatingSpeed * Time.deltaTime);
    }

    private GameObject hmt;
    void isActionFunc(){
        isAction = true;

        target = new Vector3
        (GameObject.FindWithTag("player").GetComponent<PlayerMovement>().movePoint.position.x ,
        GameObject.FindWithTag("player").GetComponent<PlayerMovement>().movePoint.position.y ,
        GameObject.FindWithTag("player").GetComponent<PlayerMovement>().movePoint.position.z);
        
        hmt = Instantiate(TargetPrefab , target , Quaternion.identity);
    }

    void Action(){
        if(!isAction) return;
        
        Vector2 dir = (Vector2)target - rb.position;
        dir.Normalize();
        float rotateAmount = Vector3.Cross(dir , transform.up).z;
        rb.angularVelocity = -rotateAmount * rotatingSpeed;
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("player")){
            if(!canDamage) return;
            col.GetComponent<PlayerGeneral>().TakeDamage();
            Explode();
        }
    }

    void OnTriggerStay2D(Collider2D col){
        if(col.gameObject.CompareTag("player")){
            if(!canDamage) return;
            col.GetComponent<PlayerGeneral>().TakeDamage();
            Explode();
        }
    }

    void CheckForTarget(){
        float distance = Vector3.Distance(transform.position , target);
        if(distance <= 0.1f){
            Explode();
        }
        if(distance <= 0.4f){
            canDamage = true;
        }
    }

    void Explode(){
        CameraShake.SharedInstance.BigShake();

        GameObject go = Instantiate(ExplosionPrefab , transform.position , transform.rotation);

        //GameObject go2 = Instantiate(RemainingFirePrefab , transform.position , Quaternion.identity);
        
        //Destroy(go2 , remainingFireTimer);

        Destroy(go , 0.5f);
        
        Destroy(hmt);
        
        Destroy(gameObject);
    }
}
