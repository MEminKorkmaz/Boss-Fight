using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcebenderAction : MonoBehaviour
{
    public BenderGameManager benderGameManager;
    public IcebenderMovement icebenderMovement;
    public IcebenderAttack icebenderAttack;

    public GameObject Firebender;

    public bool isChanneling;

    public float DistanceRequiredToMoveToEnemy;
    private float DistanceToEnemy;

    public float ActionTime;
    private float TempActionTime;

    private int SkillIndex;
    private int PreviousSkillIndex;




    void Awake()
    {
        icebenderMovement = GetComponent<IcebenderMovement>();
        icebenderAttack = GetComponent<IcebenderAttack>();
        Firebender = GameObject.FindWithTag("Firebender");

        benderGameManager = GameObject.FindWithTag("GameManager").GetComponent<BenderGameManager>();
    }


    void Start()
    {
        PreviousSkillIndex = 99;
    }


    void Update()
    {
        if(benderGameManager.isGameOver)
            return;
        
        // if(!isChanneling)
        //     TempActionTime -= Time.deltaTime;
        
        // if(TempActionTime <= 0)
        // {
        //     if(isChanneling)
        //     {
        //         TempActionTime = ActionTime;
        //         return;
        //     }

        //     SkillIndex = Random.Range(0 , 3);
        //     while(SkillIndex == PreviousSkillIndex)
        //         SkillIndex = Random.Range(0 , 3);

        //     if(SkillIndex == 0)
        //         icebenderAttack.WallShoot();
        //     else if(SkillIndex == 1)
        //         icebenderAttack.FreezingGround();
        //     else if(SkillIndex == 2)
        //         icebenderAttack.IceBullet();
            
        //     PreviousSkillIndex = SkillIndex;
        //     SkillIndex = 99;
            
        //     TempActionTime = ActionTime;
        // }
        if(isChanneling) return;

        if(Input.GetKeyDown(KeyCode.G))
        {
            icebenderAttack.WallShoot();
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            icebenderAttack.FreezingGround();
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            icebenderAttack.IceBullet();
        }

        // MOVEMENT ********** MOVEMENT
        DistanceToEnemy = Vector3.Distance(transform.position , Firebender.transform.position);

        if(DistanceToEnemy >= DistanceRequiredToMoveToEnemy)
        {
            icebenderMovement.isMoving = true;
        }

        else
        {
            icebenderMovement.isMoving = false;
        }
        // MOVEMENT ********** MOVEMENT

        if(icebenderMovement.isMoving) return;
        
        if(!isChanneling)
            TempActionTime -= Time.deltaTime;
        
        if(TempActionTime <= 0)
        {
            if(isChanneling)
            {
                TempActionTime = ActionTime;
                return;
            }

            SkillIndex = Random.Range(0 , 3);
            while(SkillIndex == PreviousSkillIndex)
                SkillIndex = Random.Range(0 , 3);

            if(SkillIndex == 0)
                icebenderAttack.WallShoot();
            else if(SkillIndex == 1)
                icebenderAttack.FreezingGround();
            else if(SkillIndex == 2)
                icebenderAttack.IceBullet();
            
            PreviousSkillIndex = SkillIndex;
            SkillIndex = 99;
            
            TempActionTime = ActionTime;
        }
    }
}
