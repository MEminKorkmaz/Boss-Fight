using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcebenderMovement : MonoBehaviour
{
    public BenderGameManager benderGameManager;
    public IcebenderAction icebenderAction;
    public IcebenderAttack icebenderAttack;
    public GameObject Enemy;
    public Rigidbody2D rb;
    public Animator anim;
    public float Speed;
    public bool isFacingRight;
    private Vector2 Movement;

    [HideInInspector]
    public bool isMoving;




    void Awake()
    {
        icebenderAction = GetComponent<IcebenderAction>();
        icebenderAttack = GetComponent<IcebenderAttack>();

        anim = GetComponent<Animator>();

        Enemy = GameObject.FindWithTag("Firebender");

        benderGameManager = GameObject.FindWithTag("GameManager").GetComponent<BenderGameManager>();
    }

    void Start()
    {
        Physics2D.IgnoreCollision(Enemy.GetComponent<Collider2D>() , GetComponent<Collider2D>());

        if(transform.position.x < Enemy.transform.position.x)
            Movement = new Vector2(1f , 0f);
        
        else
            Movement = new Vector2(-1f , 0f);
        
        if(transform.localScale.x > 0)
            isFacingRight = true;
        else if(transform.localScale.x < 0)
            isFacingRight = false;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        if(benderGameManager.isGameOver) return;
        if(icebenderAction.isChanneling) return;
        // Move();

        if(isFacingRight && transform.position.x >= Enemy.transform.position.x)
            Flip();
        else if(!isFacingRight && transform.position.x <= Enemy.transform.position.x)
            Flip();
        
        if(transform.position.x < Enemy.transform.position.x)
            Movement = new Vector2(1f , 0f);

        else if(transform.position.x > Enemy.transform.position.x)
            Movement = new Vector2(-1f , 0f);

        
        if(isFacingRight && Movement.x == -1f)
            Flip();
        else if(!isFacingRight && Movement.x == 1f)
            Flip();
        
        if(!isMoving || icebenderAction.isChanneling)
            anim.SetFloat("Move" , 0f);
        else
            anim.SetFloat("Move" , 1f);
    }

    void Move()
    {
        if(!isMoving || icebenderAction.isChanneling)
        {
            // anim.SetFloat("Move" , 0f);
            rb.velocity = new Vector2(0f , 0f);
            return;
        }

        // anim.SetFloat("Move" , 1f);

        // if(transform.position.x < Enemy.transform.position.x)
        //     Movement = new Vector2(1f , 0f);

        // else if(transform.position.x > Enemy.transform.position.x)
        //     Movement = new Vector2(-1f , 0f);

        
        // if(isFacingRight && Movement.x == 1f)
        //     Flip();
        // else if(!isFacingRight && Movement.x == -1f)
        //     Flip();

        rb.velocity = Movement * Speed * Time.fixedDeltaTime;
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
