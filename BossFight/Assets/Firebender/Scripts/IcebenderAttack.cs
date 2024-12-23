using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcebenderAttack : MonoBehaviour
{
    public IcebenderAction icebenderAction;
    public IcebenderMovement icebenderMovement;
    public Shaker shaker;
    public Animator anim;
    public GameObject Enemy;

    [Header("Wall Shoot")]
    public GameObject WallShootPrefab;
    public GameObject WallShootCastFxPrefab;
    public int WallShootOrbAmount;
    public float WallShootOffsetForTarget;
    public float WallShootCastDelay;
    
    [Header("Freezing Ground")]
    public GameObject FreezingGroundPrefab;
    public GameObject FreezingGroundCastFxPrefab;

    public int FreezingGroundAmount;
    private int TempFreezingGroundAmount;
    private float FreezingGroundOffsetX;
    private bool isMovingForFreezeGround;
    public float FreezingGroundMoveTime;
    private float TempFreezingGroundMoveTime;
    public float FreezingGroundMoveSpeed;
    private Vector2 FreezingGroundTargetPositionInAir;
    private Vector2 FreezingGroundTargetPositionOnGround;
    private float DistanceToAir;
    private float DistanceToGround;
    public float FreezingGroundDelay;
    public int FreezingGroundJumpAmount;
    private int TempFreezingGroundJumpAmount;
    public float FreezingGroundCastDelay;

    [Header("Ice Bullet")]
    public GameObject IceBulletPrefab;

    public int IceBulletAmount;
    private int TempIceBulletAmount;

    public float IceBulletPosOffsetX;

    public float IceBulletDelay;

    public float IceBulletCastDelay;



    void Awake()
    {
        icebenderAction = GetComponent<IcebenderAction>();
        icebenderMovement = GetComponent<IcebenderMovement>();
        shaker = GameObject.FindWithTag("MainCamera").GetComponent<Shaker>();
        anim = GetComponent<Animator>();
        Enemy = GameObject.FindWithTag("Firebender");
    }

    void Start()
    {
        
    }


    void Update()
    {
        // Update is for movement in that script anyway
        if(isMovingForFreezeGround)
            FreezingGroundMovementPhase();
    }

    void ChannelingFalse()
    {
        icebenderAction.isChanneling = false;
    }

    public void WallShoot()
    {
        GameObject go = Instantiate(WallShootCastFxPrefab , transform.position , Quaternion.identity);
        Destroy(go , 0.5f);

        anim.SetTrigger("WallShoot");

        icebenderAction.isChanneling = true;

        Invoke(nameof(WallShootMainFunc) , WallShootCastDelay);
    }

    void WallShootMainFunc()
    {
        float OffsetForRotation = 0f;
        for(int i = 0; i < WallShootOrbAmount; i++)
        {
            float RndOffset = Random.Range(-WallShootOffsetForTarget , WallShootOffsetForTarget);

            GameObject go = Instantiate(WallShootPrefab ,
            new Vector3(transform.position.x , transform.position.y + 2f , 0f) ,
            Quaternion.Euler(0f , 0f , -90f + OffsetForRotation));

            go.GetComponent<IcebenderWallShootProjectile>().TargetPosition =
            new Vector2(Enemy.transform.position.x + RndOffset , Enemy.transform.position.y);

            OffsetForRotation += 180f / WallShootOrbAmount;
        }
        // icebenderAction.isChanneling = false;
        ChannelingFalse();
    }

    public void FreezingGround()
    {
        anim.SetTrigger("JumpAndDown");

        icebenderAction.isChanneling = true;

        GameObject go = Instantiate(FreezingGroundCastFxPrefab ,
        transform.position + new Vector3(-0.5f , -1.25f , 0f) , transform.rotation);
        Destroy(go , 1f);

        GameObject go2 = Instantiate(FreezingGroundCastFxPrefab ,
        transform.position + new Vector3(0.25f , -1.25f , 0f) , transform.rotation);
        Destroy(go2 , 1f);

        TempFreezingGroundJumpAmount = Random.Range(FreezingGroundJumpAmount - 1 , FreezingGroundJumpAmount + 2);

        InvokeRepeating(nameof(FreezingGroundPreMovementPhase) , FreezingGroundCastDelay , FreezingGroundDelay);
    }
    void FreezingGroundPreMovementPhase()
    {
        anim.SetTrigger("JumpAndDown");

        icebenderAction.isChanneling = true;

        if(Enemy == null) return;
        
        if(Enemy.transform.position.x <= transform.position.x)
        {
            FreezingGroundTargetPositionInAir =
            new Vector2((Enemy.transform.position.x + transform.position.x) / 2f , 2f);
            FreezingGroundTargetPositionOnGround =
            new Vector2(Enemy.transform.position.x + 2f , -5.75f);
        }

        else if(Enemy.transform.position.x > transform.position.x)
        {
            FreezingGroundTargetPositionInAir =
            new Vector2((Enemy.transform.position.x + transform.position.x) / 2f , 2f);
            FreezingGroundTargetPositionOnGround =
            new Vector2(Enemy.transform.position.x - 2f , -5.75f);
        }
        
        isMovingForFreezeGround = true;

        DistanceToAir = Vector2.Distance(transform.position , FreezingGroundTargetPositionInAir);

        DistanceToGround = Vector2.Distance(FreezingGroundTargetPositionInAir , FreezingGroundTargetPositionOnGround);

        FreezingGroundMoveTime = (DistanceToAir + DistanceToGround) / FreezingGroundMoveSpeed;
        TempFreezingGroundMoveTime = FreezingGroundMoveTime;

        TempFreezingGroundJumpAmount--;

        if(TempFreezingGroundJumpAmount <= 0)
        {
            CancelInvoke(nameof(FreezingGroundPreMovementPhase));
            // icebenderAction.isChanneling = false;
            // ChannelingFalse();
            Invoke(nameof(ChannelingFalse) , 0.75f);
        }
    }

    void FreezingGroundMovementPhase()
    {
        TempFreezingGroundMoveTime -= Time.deltaTime;

        if(TempFreezingGroundMoveTime >= FreezingGroundMoveTime / 2f)
        {
            transform.position = Vector2.MoveTowards(transform.position , FreezingGroundTargetPositionInAir ,
            FreezingGroundMoveSpeed * Time.deltaTime);
        }

        else
        {
            transform.position = Vector2.MoveTowards(transform.position , FreezingGroundTargetPositionOnGround ,
            FreezingGroundMoveSpeed * Time.deltaTime);
        }

        if(TempFreezingGroundMoveTime <= 0)
        {
            isMovingForFreezeGround = false;
            FreezingGroundPreAttackPhase();
        }
    }

    public void FreezingGroundPreAttackPhase()
    {
        shaker.shakeDuration += 0.125f;
        shaker.shakeAmount = 1.25f;

        TempFreezingGroundAmount = FreezingGroundAmount;

        if(transform.localScale.x < 0)
        {
            FreezingGroundOffsetX = -1f;
            icebenderMovement.isFacingRight = false;
        }
        else if(transform.localScale.x > 0)
        {
            FreezingGroundOffsetX = 1f;
            icebenderMovement.isFacingRight = true;
        }

        InvokeRepeating(nameof(FreezingGroundAttackPhase) , 0f , 0.01f);
    }

    void FreezingGroundAttackPhase()
    {
        float FreezingGroundRotationOffsetZ = Random.Range(-45f , 45f);

        Vector2 TempTargetPos = new Vector2(transform.position.x + FreezingGroundOffsetX , -6.85f);

        GameObject go = Instantiate(FreezingGroundPrefab , TempTargetPos ,
        Quaternion.Euler(0f , 0f , FreezingGroundRotationOffsetZ));

        var state = go.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

        go.GetComponent<Animator>().Play(state.fullPathHash , 0 , Random.Range(0f , 1f));

        if(icebenderMovement.isFacingRight)
            FreezingGroundOffsetX += Random.Range(0.75f , 1f);
        else if(!icebenderMovement.isFacingRight)
            FreezingGroundOffsetX -= Random.Range(0.75f , 1f);

        TempFreezingGroundAmount--;

        if(TempFreezingGroundAmount <= 0)
        {
            // icebenderAction.isChanneling = false;
            if(Enemy == null)
                return;
            if(transform.position.x < Enemy.transform.position.x && transform.localScale.x < 0f)
            {
                icebenderMovement.isFacingRight = !icebenderMovement.isFacingRight;
                Vector3 scaler = transform.localScale;
                scaler.x *= -1;
                transform.localScale = scaler;
            }

            if(transform.position.x > Enemy.transform.position.x && transform.localScale.x > 0f)
            {
                icebenderMovement.isFacingRight = !icebenderMovement.isFacingRight;
                Vector3 scaler = transform.localScale;
                scaler.x *= -1;
                transform.localScale = scaler;
            }
            CancelInvoke(nameof(FreezingGroundAttackPhase));
        }
    }

    public void IceBullet()
    {
        anim.SetTrigger("IceBullet");
        icebenderAction.isChanneling = true;

        Invoke(nameof(IceBulletPreAttackPhase) , IceBulletCastDelay);
    }

    public void IceBulletPreAttackPhase()
    {
        TempIceBulletAmount = IceBulletAmount;

        InvokeRepeating(nameof(IceBulletAttackPhase) , 0f , IceBulletDelay);
    }

    void IceBulletAttackPhase()
    {
        float TempIceBulletPosOffsetX = Random.Range(-IceBulletPosOffsetX , IceBulletPosOffsetX);

        // GameObject go = Instantiate(IceBulletPrefab ,
        // new Vector2(Enemy.transform.position.x + TempIceBulletPosOffsetX , Random.Range(5f , 6f)) ,
        // Quaternion.Euler(0f , 0f , Random.Range(0f + 180f , 0f + 180f)));

        GameObject go = Instantiate(IceBulletPrefab ,
        new Vector2(transform.position.x + TempIceBulletPosOffsetX , 3f) ,
        Quaternion.identity);

        if(Enemy == null)
            return;
        
        float offset = Random.Range(-5f , 5f);
        offset += -90f;
        Vector3 tempDir = (Enemy.transform.position - go.transform.position).normalized;
        float tempAngle = Mathf.Atan2(tempDir.y , tempDir.x) * Mathf.Rad2Deg;
        go.transform.rotation = Quaternion.Euler(0f , 0f , (tempAngle + offset));

        TempIceBulletAmount--;

        if(TempIceBulletAmount <= 0)
        {
            icebenderAction.isChanneling = false;
            CancelInvoke(nameof(IceBulletAttackPhase));
        }
    }
}
