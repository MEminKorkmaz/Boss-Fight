using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMonster : MonoBehaviour
{
    public Rigidbody2D rb;

    public Transform FirePoint;

    private Transform FirePointTransform;

    public GameObject ProjectilePrefab;

    public Transform Target;

    public float speed;
    
    public float minFireRate;
    public float maxFireRate;
    private float fireRate;

    public float bulletSpread;

    private bool isFacingRight;


    void Start()
    {
        FirePointTransform = transform.GetChild(0);
        
        Target = GameObject.FindWithTag("player").transform;

        fireRate = Random.Range(minFireRate , maxFireRate);

        StartCoroutine("Attack");
    }

    void Update(){
        if(Target == null) return;
        Move();
        Rotation();
        Flip();
    }

    IEnumerator Attack(){
        while(true){
        for (int i = 0; i < 3; i++)
        {
            bulletSpread = Random.Range(-bulletSpread , bulletSpread);

            Instantiate(ProjectilePrefab , FirePoint.position , Quaternion.Euler(FirePoint.eulerAngles.x ,
            FirePoint.eulerAngles.y , FirePoint.eulerAngles.z + bulletSpread));

            yield return new WaitForSeconds(fireRate);
        }

        for (int i = 0; i < 4; i++)
        {
            bulletSpread = Random.Range(-bulletSpread , bulletSpread);

            Instantiate(ProjectilePrefab , FirePoint.position , Quaternion.Euler(FirePoint.eulerAngles.x ,
            FirePoint.eulerAngles.y , FirePoint.eulerAngles.z + bulletSpread));

            yield return new WaitForSeconds(fireRate / 3f);
        }

        fireRate = Random.Range(minFireRate , maxFireRate);
        Debug.Log(fireRate);
        yield return new WaitForSeconds(fireRate * 5f);
        }
    }

    void Move(){
        transform.position = Vector3.MoveTowards(transform.position , Target.position , speed * Time.deltaTime);
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
}
