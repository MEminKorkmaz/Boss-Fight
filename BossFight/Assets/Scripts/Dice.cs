using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [HideInInspector]
    public int diceValue;
    
    public GameObject[] DiceFace;


    void Start()
    {
        diceValue = Random.Range(1 , 7);
        Invoke(nameof(DiceActive) , 1f);    
    }

    void DiceActive(){
        if(diceValue == 1){
            GameObject go = Instantiate(DiceFace[0] , transform.position , transform.rotation);
            Destroy(go , 15f);
            Destroy(gameObject);
        }
        if(diceValue == 2){
            GameObject go = Instantiate(DiceFace[1] , transform.position , transform.rotation);
            Destroy(go , 15f);
            Destroy(gameObject);
        }
        if(diceValue == 3){
            GameObject go = Instantiate(DiceFace[2] , transform.position , transform.rotation);
            Destroy(go , 15f);
            Destroy(gameObject);
        }
        if(diceValue == 4){
            GameObject go = Instantiate(DiceFace[3] , transform.position , transform.rotation);
            Destroy(go , 15f);
            Destroy(gameObject);
        }
        if(diceValue == 5){
            GameObject go = Instantiate(DiceFace[4] , transform.position , transform.rotation);
            Destroy(go , 15f);
            Destroy(gameObject);
        }
        if(diceValue == 6){
            GameObject go = Instantiate(DiceFace[5] , transform.position , transform.rotation);
            Destroy(go , 15f);
            Destroy(gameObject);
        }
    }
}
