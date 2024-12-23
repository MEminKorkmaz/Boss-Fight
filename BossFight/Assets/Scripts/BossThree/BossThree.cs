using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThree : MonoBehaviour
{
    public GameObject ChessPiecesPrefab;

    public GameManager gameManager;


    void Start(){
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        Vector3 Target = new Vector3(0f , -2f , 0f);
        
        Instantiate(ChessPiecesPrefab , Target , Quaternion.identity);
    }
}
