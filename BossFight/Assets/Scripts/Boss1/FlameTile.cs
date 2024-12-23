using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTile : MonoBehaviour
{
    public GameObject particleEffect;

    public SpriteRenderer sr;

    public float delayTime;
    public float destroyTime;
    private bool isOn;

    public static bool isActive;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        
        Invoke(nameof(ParticleOn2) , delayTime);
    }

    void Update(){
        if(!isActive) return;
        ParticleOn();
    }

    void ParticleOn2(){
        isActive = true;

        ParticleOn();
    }

    void ParticleOn(){
        if(!isActive) return;
        particleEffect.SetActive(true);
        sr.color = new Color(0f , 0f , 0f , 0f);
        isOn = true;
        //CameraShake.SharedInstance.LittleShake();
        //Destroy(gameObject , destroyTime);
        Invoke(nameof(DestroyObject) , destroyTime);
    }

    void DestroyObject(){
        //isActive = false;
        Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D col){
        if(col.gameObject.CompareTag("player")){
            if(!isOn) return;
            col.GetComponent<PlayerGeneral>().TakeDamage();
            isOn = false;
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.CompareTag("player")){
            isOn = true;
        }
    }
}
