using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFour : MonoBehaviour
{
    public float minAttackInterval;
    public float maxAttackInterval;
    private float attackInterval;

    [Header("Archer Tower")]
    public GameObject ArcherTowerPrefab;
    public GameObject ArcherTowerParticlePrefab;

    public Vector3[] Corners;

    [Header("Flying Monster")]
    public GameObject FlyingMonsterPrefab;

    [Header("Others")]
    public GameObject[] Enemies;

    public GameManager gameManager;

    private int enemyCount;



    void Start(){
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        attackInterval = (Random.Range(minAttackInterval , maxAttackInterval) / 2f);

        StartCoroutine("BossAttack");

    }

    
    void Update(){
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    IEnumerator BossAttack(){
        yield return new WaitForSeconds(attackInterval);
        while(!gameManager.isGameOver){

        if(enemyCount <= 0){
            yield return new WaitForSeconds(attackInterval);
            CreateArcherTower();
            //yield return new WaitForSeconds(attackInterval);
            }
            yield return null;
        }
    }

    void CreateArcherTower(){

        /*Vector3 Target1 = new Vector3(4.5f , 0.5f);
        Vector3 Target2 = new Vector3(-4.5f , -4.5f);*/

        int x = Random.Range(0 , 3);
        int y = Random.Range(0 , 3);

        while(y == x){
        y = Random.Range(0 , 3);
        }

        Instantiate(ArcherTowerPrefab , Corners[x] , Quaternion.identity);
        Instantiate(ArcherTowerPrefab , Corners[y] , Quaternion.identity);

        GameObject go = Instantiate(ArcherTowerParticlePrefab , Corners[x] , Quaternion.identity);
        GameObject go2 = Instantiate(ArcherTowerParticlePrefab , Corners[y] , Quaternion.identity);

        Destroy(go , 2f);
        Destroy(go2 , 2f);
        
        attackInterval = Random.Range(minAttackInterval , maxAttackInterval);
    }
}
