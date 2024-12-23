using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public GameObject ArrowPrefab;
    public Transform FirePoint;

    private Transform FirePointTransform;

    public Transform Target;

    public Vector3 TargetPos;

    public Animator anim;

    public float speed;

    public float bulletSpread;

    public float minFireRate;
    public float maxFireRate;
    private float fireRate;

    private bool isFacingRight;

    private bool canAttack;




    void Start(){
        FirePointTransform = transform.GetChild(0);

        Target = GameObject.FindWithTag("player").transform;

        fireRate = Random.Range(minFireRate , maxFireRate);

        float x = Random.Range(-5 , 4);
        float y = Random.Range(0 , -5);

        x += 0.5f;
        y += 0.5f;

        Mathf.RoundToInt(x);
        Mathf.RoundToInt(y);

        TargetPos = new Vector3(x , y , 0f);
    }



    void Update()
    {
        if(Target == null) return;
        Move();
        if(!canAttack) return;
        Attack();
        Rotation();
        Flip();
    }

    void Attack(){
        if(!goFire) return;
        goFire = false;

        //Instantiate(ArrowPrefab , FirePoint.position , FirePoint.rotation);
        anim.SetTrigger("Attack");

        Invoke(nameof(FireRateFunc) , fireRate);
    }

    private bool goFire = true;

    void FireRateFunc(){
        goFire = true;

        fireRate = Random.Range(minFireRate , maxFireRate);
    }

    public void CreateArrow(){
        float x = Random.Range(-bulletSpread , bulletSpread);
        
            Instantiate(ArrowPrefab , FirePoint.position , Quaternion.Euler(FirePoint.eulerAngles.x , 
            FirePoint.eulerAngles.y ,
            FirePoint.eulerAngles.z + x));
    }

    void Rotation(){
        var offset = 270f;
        Vector3 dir = (Target.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        FirePointTransform.rotation = Quaternion.Euler(0f , 0f , (angle + offset));
    }

    void Flip(){
        if(isFacingRight && transform.position.x < Target.position.x){
            isFacingRight = !isFacingRight;
            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1f;
            transform.localScale = new Vector3(tempScale.x , tempScale.y , tempScale.z);
        }

        else if(!isFacingRight && transform.position.x > Target.position.x){
            isFacingRight = !isFacingRight;
            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1f;
            transform.localScale = new Vector3(tempScale.x , tempScale.y , tempScale.z);
        }
    }

    void Move(){
        anim.SetBool("Moving" , true);
        transform.position = Vector3.MoveTowards(transform.position , TargetPos , speed * Time.deltaTime);

        if(isFacingRight && transform.position.x < TargetPos.x){
            isFacingRight = !isFacingRight;
            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1f;
            transform.localScale = new Vector3(tempScale.x , tempScale.y , tempScale.z);
        }

        else if(!isFacingRight && transform.position.x > TargetPos.x){
            isFacingRight = !isFacingRight;
            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1f;
            transform.localScale = new Vector3(tempScale.x , tempScale.y , tempScale.z);
        }

        if(transform.position == TargetPos){
            canAttack = true;
            anim.SetBool("Moving" , false);
        }
    }
}
