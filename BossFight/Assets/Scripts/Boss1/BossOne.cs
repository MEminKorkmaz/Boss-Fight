using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOne : MonoBehaviour
{
    public Transform baseTile;
    public Transform[] tiles;

    public GameObject[] AttackStyles;

    public float minAttackInterval;
    public float maxAttackInterval;
    private float attackInterval;

    private int attackIndex;

    private int usedAttackIndex;
    
    [Header("Flame Tile")]
    public GameObject FlameTilePrefab;
    public int minCount;
    public int maxCount;

    [Header("Homing Missile")]
    public GameObject HomingMissilePrefab;

    public Transform firePointRight;
    public Transform firePointLeft;

    public float missileTimer;

    public int missileCount;
    private int tempMissileCount;
    private int missileSide;

    [Header("Arrows")]
    public GameObject FireArrowPrefab;
    
    public int fireArrowCount;
    private int tempFireArrowCount;

    public float fireArrowTimer;

    [Header("Halloween Head")]
    public GameObject HalloweenHeadPrefab;

    public int halloweenTileJump;

    public int[] halloweenPositionList;
    public int halloweenHeadCount;

    public int[] halloweenPositionList1;
    public int halloweenHeadCount1;

    public int[] halloweenPositionList2;
    public int halloweenHeadCount2;

    public float halloweenHeadDelay;// 0.05f;

    public GameManager gameManager;


    void Update(){
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            //CreateFlameTile();
            StartCoroutine("CreateFlameTile");
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            CreateHomingMissile();
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            StartCoroutine("CreateFireArrows");
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            StartCoroutine("CreateHalloweenHead");
        }
    }


    void Start(){
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        attackInterval = (Random.Range(minAttackInterval , maxAttackInterval) / 2f);
        
        // StartCoroutine("BossAttack");

        tempMissileCount = 0;
        tempFireArrowCount = fireArrowCount;

        baseTile = GameObject.FindWithTag("BaseTile").transform;
        for(int i = 0; i < 60; i++){
            tiles[i] = baseTile.GetChild(i);
        }
    }

    private int ry;
    IEnumerator BossAttack(){
        yield return new WaitForSeconds(attackInterval);
        while(!gameManager.isGameOver){
            while(attackIndex == usedAttackIndex){
            attackIndex = Random.Range(0 , 4);
            }
            if(attackIndex == 0){
                CreateHomingMissile();
                usedAttackIndex = attackIndex;
                attackInterval = Random.Range(minAttackInterval , maxAttackInterval);
                yield return new WaitForSeconds(attackInterval);
                }
            else if(attackIndex == 1){
                StartCoroutine("CreateFlameTile");
                usedAttackIndex = attackIndex;
                attackInterval = Random.Range(minAttackInterval , maxAttackInterval);
                yield return new WaitForSeconds(attackInterval);
                }
            else if(attackIndex == 2){
                StartCoroutine("CreateFireArrows");
                usedAttackIndex = attackIndex;
                attackInterval = Random.Range(minAttackInterval , maxAttackInterval);
                yield return new WaitForSeconds(attackInterval);
                }
            else if(attackIndex == 3){
                StartCoroutine("CreateHalloweenHead");
                usedAttackIndex = attackIndex;
                attackInterval = Random.Range(minAttackInterval , maxAttackInterval);
                yield return new WaitForSeconds(attackInterval);
                }
        }
    }

    List<int> usedValues = new List<int>();
    IEnumerator CreateFlameTile(){
        
        int rnd = Random.Range(minCount , maxCount);

        for(int i = 0; i < rnd; i++){
            int rndTile = Random.Range(0 , 60);
            while(usedValues.Contains(rndTile)){
                rndTile = Random.Range(0 , 60);
            }
            yield return new WaitForSeconds(0.01f);
            usedValues.Add(rndTile);
            Instantiate(FlameTilePrefab , tiles[rndTile].position , Quaternion.identity);
            FlameTile.isActive = false;
        }
        usedValues.Clear();
    }

    void CreateHomingMissile(){
        InvokeRepeating(nameof(HomingMissileCall) , 0f , missileTimer);
    }

    private float offsetX = -10;
    private float offsetY = -10;
    void HomingMissileCall(){
        if(missileSide % 2 == 0){
                Instantiate(HomingMissilePrefab , firePointRight.position , Quaternion.Euler(firePointRight.eulerAngles.x ,
                firePointRight.eulerAngles.y , firePointRight.eulerAngles.z + offsetX));
                missileSide++;
                tempMissileCount++;
                offsetX += 10f;
            }
            else{
                Instantiate(HomingMissilePrefab , firePointLeft.position , Quaternion.Euler(firePointLeft.eulerAngles.x ,
                firePointLeft.eulerAngles.y , firePointLeft.eulerAngles.z + offsetY));
                missileSide = 0;
                tempMissileCount++;
                offsetY += 10f;
            }

        if(tempMissileCount >= missileCount){
            CancelInvoke("HomingMissileCall");
            tempMissileCount = 0;
        }

        offsetX = -10f;
        offsetY = -10f;
    }

    IEnumerator CreateFireArrows(){
        for(int i = 0; i < tempFireArrowCount; i++){
        float xA = 0f;
        int x = Random.Range(0 , 2);//-5.5
        int y = Random.Range(-4 , 2);

        if (x == 0){
        xA = -5.5f;
        Vector3 target = new Vector3(xA , y - 0.5f , 0f);
        GameObject go = Instantiate(FireArrowPrefab , target , Quaternion.Euler(0f , 0f , 270f));
        tempFireArrowCount--;
        }

        else{
        xA = 5.5f;
        Vector3 target = new Vector3(xA , y - 0.5f , 0f);
        GameObject go = Instantiate(FireArrowPrefab , target , Quaternion.Euler(0f , 0f , 90f));
        tempFireArrowCount--;
            }
        yield return new WaitForSeconds(fireArrowTimer);
        }
        tempFireArrowCount = fireArrowCount;
    }

    IEnumerator CreateHalloweenHead(){
        int x = Random.Range(0 , 3);

        if(x == 0){
        for(int i = 0; i < halloweenHeadCount; i ++){
        int pos = halloweenPositionList[i];
        GameObject go = Instantiate(HalloweenHeadPrefab , tiles[pos].position , Quaternion.identity);
        yield return new WaitForSeconds(halloweenHeadDelay);
        }
        }

        else if(x == 1){
        for(int i = 0; i < halloweenHeadCount1; i ++){
        int pos = halloweenPositionList1[i];
        GameObject go = Instantiate(HalloweenHeadPrefab , tiles[pos].position , Quaternion.identity);
        yield return new WaitForSeconds(halloweenHeadDelay);
        }
        }

        else if(x == 2){
        for(int i = 0; i < halloweenHeadCount2; i ++){
        int pos = halloweenPositionList2[i];
        GameObject go = Instantiate(HalloweenHeadPrefab , tiles[pos].position , Quaternion.identity);
        yield return new WaitForSeconds(halloweenHeadDelay);
        }
        }
    }
}
