using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalloweenHead : MonoBehaviour
{

    public GameObject ExplosionPrefab;

    public SpriteRenderer sr;

    public float attackDelay;// 2.4



    void Start(){
        sr = GetComponent<SpriteRenderer>();

        Invoke(nameof(Attack) , attackDelay);
    }

    void Attack(){
        int x = Random.Range(0 , 2);
        GameObject go = Instantiate(ExplosionPrefab , transform.position , transform.rotation);

        go.transform.parent = this.transform;
        
        sr.enabled = false;
        Destroy(go , 1.5f);
        Destroy(gameObject , 1f);
    }
}
