using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake SharedInstance;
    
    public Animator anim;

    void Awake(){
        SharedInstance = this;

        anim = GetComponent<Animator>();
    }

    public void BigShake(){
        anim.SetTrigger("BigShake");
    }

    public void LittleShake(){
        anim.SetTrigger("LittleShake");
    }

    public void LevelStart(){
        anim.SetTrigger("LevelStart");
    }
}
