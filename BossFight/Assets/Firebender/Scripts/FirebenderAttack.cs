using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebenderAttack : MonoBehaviour
{
    public FirebenderMovement firebenderMovement;
    public Rigidbody2D rb;
    public Animator anim;
    public Transform SideFirePoint;
    public Transform TopFirePoint;
    public Shaker shaker;
    private bool isChanneling;

    [Header("Standart Attack")]
    public GameObject StandartAttackOrbPrefab;

    public float AttackCooldown;
    private float TempAttackCooldown;

    public bool isAttacking;
    private bool isAimingUp;

    public float ProjectileSpread;

    [Header("Dash")]

    public float DashCooldown;
    private float TempDashCooldown;

    public float DashSpeed;
    public float DashTime;
    private float TempDashTime;

    private bool isDashing;

    [Header("FireBlock")]
    public GameObject FireBlockPrefab;

    public float FireBlockCooldown;
    private float TempFireBlockCooldown;
    private bool isFireBlockAimingUp;

    [Header("JumpAndDown")]

    public float JumpAndDownCooldown; // cooldown
    private float TempJumpAndDownCooldown;

    public float JumpAndDownSpeed;
    public float JumpAndDownTime; // how long will it take effect
    private float TempJumpAndDownTime;

    private bool isJumpAndDown;

    public GameObject LeftParticle;
    public GameObject RightParticle;

    public GameObject GroundParticlePrefab;
    public GameObject JumpAndDownFxPrefab;

    public int JumpAndDownDamage;

    public float JumpAndDownAreaRadius;


    void Awake()
    {
        shaker = GameObject.FindWithTag("MainCamera").GetComponent<Shaker>();

        firebenderMovement = GetComponent<FirebenderMovement>();

        anim = GetComponent<Animator>();

        TempDashTime = DashTime;
        TempJumpAndDownTime = JumpAndDownTime;
    }


    void Start()
    {
        var LeftPS = LeftParticle.GetComponent<ParticleSystem>().emission;
        var RightPS = RightParticle.GetComponent<ParticleSystem>().emission;

        LeftPS.enabled = false;
        RightPS.enabled = false;
    }


    void Update()
    {
        TempAttackCooldown -= Time.deltaTime;
        TempDashCooldown -= Time.deltaTime;
        TempFireBlockCooldown -= Time.deltaTime;
        TempJumpAndDownCooldown -= Time.deltaTime;

        if(TempAttackCooldown <= 0)
        {
            if(Input.GetKey(KeyCode.W) && !isAttacking && !isChanneling) // input for attack
            {
                if(Input.GetKey(KeyCode.Space))
                {
                    // shaker.shakeDuration += 0.05f;
                    // shaker.shakeAmount = 0.1f;

                    isAimingUp = true;

                    StandartAttack();

                    TempAttackCooldown = AttackCooldown;
                }
            }

            else if(Input.GetKey(KeyCode.Space) && !isAttacking && !isChanneling) // input for attack
            {
                isAimingUp = false;

                StandartAttack();

                TempAttackCooldown = AttackCooldown;
            }
        }

        if(TempDashCooldown <= 0)
        {
            if(Input.GetKeyDown(KeyCode.Q) && !isChanneling) // input for dash
            {
                isDashing = true;
            }

            if(isDashing)
            {
                isChanneling = true;
                Dash();

                if(!isDashing)
                {
                    TempDashCooldown = DashCooldown;
                    isChanneling = false;
                }
            }
        }

        if(TempFireBlockCooldown <= 0f)
        {
            if(Input.GetKey(KeyCode.W))
            {
                if(Input.GetKeyDown(KeyCode.Alpha1)  && !isChanneling)
                {
                    isFireBlockAimingUp = true;
                    FireBlock();

                    shaker.shakeDuration += 0.2f;
                    shaker.shakeAmount = 0.5f;

                    TempFireBlockCooldown = FireBlockCooldown;
                }
            }

            else if(Input.GetKeyDown(KeyCode.Alpha1) && !isChanneling)
            {
                isFireBlockAimingUp = false;
                FireBlock();

                shaker.shakeDuration += 0.2f;
                shaker.shakeAmount = 0.5f;

                TempFireBlockCooldown = FireBlockCooldown;
            }
        }

        if(TempJumpAndDownCooldown <= 0)
        {
            if(Input.GetKeyDown(KeyCode.Alpha2) && !isChanneling)
            {
                anim.SetTrigger("JumpAndDown");
                isJumpAndDown = true;
            }

            // if(isJumpAndDown)
            // {
            //     isChanneling = true;
            //     JumpAndDown();

            //     if(!isJumpAndDown)
            //     {
            //         TempJumpAndDownCooldown = JumpAndDownCooldown;
            //         isChanneling = false;
            //     }
            // }
        }
    }

    void FixedUpdate()
    {
        if(isJumpAndDown)
            {
                isChanneling = true;
                JumpAndDown();

                if(!isJumpAndDown)
                {
                    TempJumpAndDownCooldown = JumpAndDownCooldown;
                    isChanneling = false;
                }
            }
    }

    void StandartAttack()
    {
        if(!isAimingUp)
            anim.SetTrigger("Attack");

        else
            anim.SetTrigger("AimingUpAttack");

        isAttacking = true;

        Invoke(nameof(StandartAttackMainFunc) , AttackCooldown);
    }

    void StandartAttackMainFunc()
    {
        isAttacking = false;

        float TempProjectileSpread = Random.Range(-ProjectileSpread , ProjectileSpread);

        if(!isAimingUp)
        {
            GameObject go = Instantiate(StandartAttackOrbPrefab , SideFirePoint.position , Quaternion.Euler(SideFirePoint.eulerAngles.x ,
            SideFirePoint.eulerAngles.y , SideFirePoint.eulerAngles.z + TempProjectileSpread));
            Destroy(go , 15f);
        }

        else
        {
            GameObject go = Instantiate(StandartAttackOrbPrefab , TopFirePoint.position , Quaternion.Euler(TopFirePoint.eulerAngles.x ,
            TopFirePoint.eulerAngles.y , TopFirePoint.eulerAngles.z + TempProjectileSpread));
            Destroy(go , 15f);
        }
    }

    void Dash()
    {
        anim.SetTrigger("Dash");

        if(transform.localScale.x > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position ,
            new Vector2(transform.position.x + 15f , transform.position.y) , DashSpeed * Time.deltaTime);
        }

        else
        {
            transform.position = Vector2.MoveTowards(transform.position ,
            new Vector2(transform.position.x - 15f , transform.position.y) , DashSpeed * Time.deltaTime);
        }

        TempDashTime -= Time.deltaTime;

        if(TempDashTime <= 0)
        {
            TempDashTime = DashTime;
            isDashing = false;
        }
    }

    void FireBlock()
    {
        anim.SetTrigger("FireBlock");

        if(isFireBlockAimingUp)
        {
            GameObject go = Instantiate(FireBlockPrefab , TopFirePoint.position , TopFirePoint.rotation);

            Destroy(go , 1f);
        }

        else
        {
            GameObject go = Instantiate(FireBlockPrefab ,
            SideFirePoint.position + new Vector3(0f , 5.75f , 0f) , SideFirePoint.rotation);

            Destroy(go , 1f);
        }

        // Destroy(go , 1f); // was 0.75
    }

    void JumpAndDown()
    {
        var LeftPS = LeftParticle.GetComponent<ParticleSystem>().emission;
        var RightPS = RightParticle.GetComponent<ParticleSystem>().emission;
        
        LeftPS.enabled = true;
        RightPS.enabled = true;

        TempJumpAndDownTime -= Time.deltaTime;

        if(TempJumpAndDownTime >= (JumpAndDownTime * 2f) / 3f)
        {
            rb.velocity = (transform.up * 3f) * JumpAndDownSpeed * Time.deltaTime;
        }

        else if(TempJumpAndDownTime <= (JumpAndDownTime * 2f) / 3f && TempJumpAndDownTime >= JumpAndDownTime / 3f) // Going down to stun
        {
            rb.velocity = -transform.up * JumpAndDownSpeed * Time.fixedDeltaTime;
        }

        else if(TempJumpAndDownTime <= JumpAndDownTime / 3f)
        {
            shaker.shakeDuration += 0.125f;
            shaker.shakeAmount = 0.75f;

            GameObject go = Instantiate(GroundParticlePrefab ,
            new Vector3(transform.position.x , transform.position.y - 1.1f , 0f) , Quaternion.identity);
            Destroy(go , 2f);

            GameObject go2 = Instantiate(JumpAndDownFxPrefab , new Vector3(transform.position.x , -6.8f , 0f) ,
            Quaternion.identity);
            Destroy(go2 , 2f);

            TempJumpAndDownTime = JumpAndDownTime;
            isJumpAndDown = false;

            LeftPS.enabled = false;
            RightPS.enabled = false;

            Collider2D[] AreaCheck = Physics2D.OverlapCircleAll(transform.position , JumpAndDownAreaRadius);

            foreach (Collider2D collider in AreaCheck)
            {
                if(collider.gameObject.CompareTag("Icebender"))
                {
                    collider.GetComponent<TakeDamageEnemy>().TakeDamage(JumpAndDownDamage);
                }
            }
        }
    }
}
