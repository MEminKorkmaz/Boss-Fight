using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleLasers : MonoBehaviour
{

    public Rigidbody2D rb;

    public float speed;

    public float actionDelay;

    private bool isAction;



    void Start()
    {
        Invoke(nameof(Action) , actionDelay);
    }


    void FixedUpdate()
    {
        if(!isAction) return;
        Move();
    }

    void Move(){
        rb.velocity = transform.up * speed * Time.fixedDeltaTime;
    }

    void Action(){
        isAction = true;
    }
}
