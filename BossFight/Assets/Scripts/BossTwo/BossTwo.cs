using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwo : MonoBehaviour
{
    public Transform baseTile;
    public Transform[] tiles;

    public float minAttackInterval;
    public float maxAttackInterval;
    private float attackInterval;

    private int attackIndex;

    private int usedAttackIndex;

    public GameManager gameManager;

    [Header("Shooter")]
    public GameObject ShootingOrbPrefab;
    public float shootingOrbDelay;
    public int shootingOrbAmount;
    public Vector3 ShootingOrbTarget;

    [Header("Dividing")]
    public GameObject DividingOrbPrefab;
    public float dividingOrbDelay;
    public int dividingOrbAmount;

    [Header("Turning Shooter")]
    public GameObject TurningShooterOrbPrefab;
    public float turningShooterOrbDelay;
    public int turningShooterOrbAmount;

    [Header("Triangle Lasers")]
    public GameObject TriangleLaserOpenPrefab;
    public GameObject TriangleLaserClosePrefab;
    public float triangleLaserDelay;
    public int triangleLaserAmount;



    void Start(){
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        attackInterval = (Random.Range(minAttackInterval , maxAttackInterval) / 2f);

        //StartCoroutine("CreateShootingOrb");
        StartCoroutine("BossAttack");

        baseTile = GameObject.FindWithTag("BaseTile").transform;
        for(int i = 0; i < 60; i++){
            tiles[i] = baseTile.GetChild(i);
        }
        //StartCoroutine("CreateDividingOrb");
        //StartCoroutine("CreateTurningShooterOrb");
        //StartCoroutine("CreateTriangleLasers");
    }

    IEnumerator BossAttack(){
        yield return new WaitForSeconds(attackInterval);
        while(!gameManager.isGameOver){
            while(attackIndex == usedAttackIndex){
            attackIndex = Random.Range(0 , 4);
            }
            if(attackIndex == 0){
                StartCoroutine("CreateShooterOrb");
                usedAttackIndex = attackIndex;
                attackInterval = Random.Range(minAttackInterval , maxAttackInterval);
                yield return new WaitForSeconds(attackInterval);
                }
            else if(attackIndex == 1){
                StartCoroutine("CreateDividingOrb");
                usedAttackIndex = attackIndex;
                attackInterval = Random.Range(minAttackInterval , maxAttackInterval);
                yield return new WaitForSeconds(attackInterval);
                }
            else if(attackIndex == 2){
                StartCoroutine("CreateTurningShooterOrb");
                usedAttackIndex = attackIndex;
                attackInterval = Random.Range(minAttackInterval , maxAttackInterval);
                yield return new WaitForSeconds(attackInterval);
                }
            else if(attackIndex == 3){
                StartCoroutine("CreateTriangleLasers");
                usedAttackIndex = attackIndex;
                attackInterval = Random.Range(minAttackInterval , maxAttackInterval);
                yield return new WaitForSeconds(attackInterval);
                }
        }
    }

    IEnumerator CreateShooterOrb(){
        for(int i = 0; i < shootingOrbAmount; i++){

        int rnd = Random.Range(0 , 2);

        float x;
        float y;

        if(rnd == 0){
        x = Random.Range(-4 , -2);
        y = Random.Range(-5 , 0);
        }

        else{
        x = Random.Range(1 , 3);
        y = Random.Range(-5 , 0);
        }

        x += 0.5f;
        y += 0.5f;

        Mathf.RoundToInt(x);
        Mathf.RoundToInt(y);

        ShootingOrbTarget = new Vector3(x , y , 0f);
        Vector3 ShootingOrbTarget2 = new Vector3(-x , ShootingOrbTarget.y , 0f);

        Instantiate(ShootingOrbPrefab , ShootingOrbTarget , Quaternion.identity);
        Instantiate(ShootingOrbPrefab , ShootingOrbTarget2 , Quaternion.identity);

        yield return new WaitForSeconds(shootingOrbDelay);
        }
    }
    
    IEnumerator CreateDividingOrb(){
        for(int i = 0; i < dividingOrbAmount; i++){

        int x = Random.Range(0 , 60);
        int y = Random.Range(0 , 60);
        int z = Random.Range(0 , 60);
        int v = Random.Range(0 , 60);

        Instantiate(DividingOrbPrefab , tiles[x].position , Quaternion.identity);
        Instantiate(DividingOrbPrefab , tiles[y].position , Quaternion.identity);
        Instantiate(DividingOrbPrefab , tiles[z].position , Quaternion.identity);
        Instantiate(DividingOrbPrefab , tiles[v].position , Quaternion.identity);

        yield return new WaitForSeconds(dividingOrbDelay);
        }
    }

    IEnumerator CreateTurningShooterOrb(){
        
        for(int i = 0; i < turningShooterOrbAmount; i++){

        float x = Random.Range(-3 , 3);
        float y = Random.Range(-3 , -1);

        x += 0.5f;
        y += 0.5f;

        Mathf.RoundToInt(x);
        Mathf.RoundToInt(y);

        Vector3 TurningShooterOrbTarget = new Vector3(x , y , 0f);
        
        Instantiate(TurningShooterOrbPrefab , TurningShooterOrbTarget , Quaternion.identity);

        yield return new WaitForSeconds(turningShooterOrbDelay);
        }
    }

    IEnumerator CreateTriangleLasers(){
        Vector3 LeftTarget = new Vector3(-2.5f , 3f , 0f);
        Vector3 RightTarget = new Vector3(2.5f , 3f , 0f);

        for (int i = 0; i < triangleLaserAmount; i++){
            int x = Random.Range(0 , 2);
            if(x == 0){
                Instantiate(TriangleLaserOpenPrefab , LeftTarget , Quaternion.Euler(0f , 0f , 180f));
                Instantiate(TriangleLaserClosePrefab , RightTarget , Quaternion.Euler(0f , 0f , 180f));
            }
            else{
                Instantiate(TriangleLaserOpenPrefab , RightTarget , Quaternion.Euler(0f , 0f , 180f));
                Instantiate(TriangleLaserClosePrefab , LeftTarget , Quaternion.Euler(0f , 0f , 180f));
            }
            yield return new WaitForSeconds(triangleLaserDelay);
        }
    }
}
