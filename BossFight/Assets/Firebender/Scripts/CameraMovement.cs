using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public BenderGameManager benderGameManager;
    public GameObject Firebender;
    public GameObject Icebender;

    public float BaseSpeed;
    private float MainSpeed;



    
    void Awake()
    {
        Firebender = GameObject.FindWithTag("Firebender");
        Icebender = GameObject.FindWithTag("Icebender");
        benderGameManager = GameObject.FindWithTag("GameManager").GetComponent<BenderGameManager>();
    }


    void Update()
    {
        if(benderGameManager.isGameOver) return;

        float MiddleDistance = 0f;
        float DistanceLevel = 0f;
        
        MiddleDistance = (Firebender.transform.position.x + Icebender.transform.position.x) / 2f;

        DistanceLevel = Vector3.Distance(Firebender.transform.position ,
        Icebender.transform.position);
        
        MainSpeed = BaseSpeed * DistanceLevel;

        transform.position = Vector3.Lerp(transform.position ,
        new Vector3(MiddleDistance , 0f , -10f) ,
        (MainSpeed * Time.deltaTime) / 100f);
    }
}
