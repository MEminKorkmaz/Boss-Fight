using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DividedOrb : MonoBehaviour
{

    public Rigidbody2D rb;

    public float speed;

    public bool canAction;

    public float actionDelay;


    void Start(){
        Invoke(nameof(ActionFunc) , actionDelay);
    }

    void ActionFunc(){
        canAction = true;
        transform.tag = "Obstacle";
    }

    void FixedUpdate()
    {
        if(!canAction) return;

        Move();
    }

    void Move(){
        rb.velocity = transform.up * speed * Time.fixedDeltaTime;
    }
}
