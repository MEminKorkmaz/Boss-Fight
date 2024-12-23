using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularOrb : MonoBehaviour
{
    public Rigidbody2D rb;
    
    public SpriteRenderer sr;

    public float speed;

    public Sprite[] sprites;

    public Vector3[] Targets;

    private Vector3 MainTarget;

    private int targetIndex;

    private int targetAmount;
    

    void Start(){

        int spriteIndex = Random.Range(0 , sprites.Length);

        sr.sprite = sprites[spriteIndex];

        targetIndex = 0;
        
        targetAmount = Targets.Length - 1;

        MainTarget = Targets[targetIndex];
    }

    void FixedUpdate(){
        //CheckForDistance();
        Move();
    }

    public void asd(){
        Debug.Log("sdfsdfsdfsdf");
    }

    void Move(){
        float distance = Vector3.Distance(transform.position , MainTarget);

        if(targetIndex >= targetAmount && distance <= 0.1f){
            Destroy(gameObject);
            return;
        }

        if(distance <= 0.1f){
            targetIndex++;
            MainTarget = Targets[targetIndex];
        }
        /*if(targetIndex > targetAmount)
        Destroy(gameObject);*/
        //rb.velocity = transform.up * speed * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position , MainTarget , speed * Time.fixedDeltaTime);
    }
}
