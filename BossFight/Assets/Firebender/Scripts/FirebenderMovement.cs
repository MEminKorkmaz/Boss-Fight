using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebenderMovement : MonoBehaviour
{
    public GameObject Enemy;
    public FirebenderAttack fireBenderAttack;
    public Animator anim;
    public Rigidbody2D rb;

    public float Speed;

    [HideInInspector]
    public Vector2 Movement;

    private bool facingRight;



    void Awake()
    {
        anim = GetComponent<Animator>();

        fireBenderAttack = GetComponent<FirebenderAttack>();

        Enemy = GameObject.FindWithTag("Icebender");
    }

    void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>() , Enemy.GetComponent<Collider2D>());
    }

    void FixedUpdate()
    {
        // move();

        if(fireBenderAttack.isAttacking)
        {
            anim.SetFloat("Move" , 0f);
            rb.velocity = new Vector2(0f , 0f);
            return;
        }
        
        Move();
    }

    void Update()
    {
        // if(fireBenderAttack.isAttacking)
        // {
        //     anim.SetFloat("Move" , 0f);
        //     rb.velocity = new Vector2(0f , 0f);
        //     return;
        // }


        float x = Input.GetAxisRaw("Horizontal");
        // float y = Input.GetAxisRaw("Vertical");
        float y = 0f;

        Movement = new Vector2(x , y);

        if(facingRight && Movement.x > 0)
        {
            Flip();
        }
        else if(!facingRight && Movement.x < 0)
        {
            Flip();
        }

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            anim.SetFloat("Move" , 1f);

        else
            anim.SetFloat("Move" , 0f);

        // Move();
    }

    void Move()
    {
        if(Movement.y < 0)
        {
            Movement = new Vector2(Movement.x , 0f);
        }

        rb.velocity = Movement * Speed * Time.fixedDeltaTime;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
