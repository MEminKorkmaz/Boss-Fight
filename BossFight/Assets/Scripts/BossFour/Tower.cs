using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public GameObject EnemyPrefab;

    public float SpawnRate;

    public GameManager gameManager;

    public int maxArcherCount;
    private int archerCount;



    void Awake(){
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }


    void Start(){
        StartCoroutine("SpawnEnemy");
    }


    IEnumerator SpawnEnemy(){
        while(!gameManager.isGameOver){
            
            float x = Random.Range(-5 , 5);
            float y = Random.Range(-5 , 0);

            x += 0.5f;
            y += 0.5f;

            Mathf.RoundToInt(x);
            Mathf.RoundToInt(y);
            
            //Vector3 SpawnPos = new Vector3(x , y , 0f);

            for(int i = 0; i <= archerCount; i++){
            Instantiate(EnemyPrefab , transform.position , Quaternion.identity);
            }
            if(archerCount < maxArcherCount){
            archerCount++;
            }

            yield return new WaitForSeconds(SpawnRate);
        }
    }
}
