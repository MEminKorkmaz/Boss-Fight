using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningShooterOrb : MonoBehaviour
{
    public GameObject DividedOrb;

    public Transform firePoint;

    public float rotatingSpeed;

    public float delayBetweenShots;
    public float doubleDelayBetweenShots;
    public float destroyTime;

    public float delayTime;



    void Start()
    {
        Invoke(nameof(DelayFunc) , delayTime);
        Destroy(gameObject , destroyTime);

        int x = Random.Range(0 , 2);
        if(x == 0)
        rotatingSpeed *= -1f;
    }

    void DelayFunc(){
        StartCoroutine("Dividing");
    }


    IEnumerator Dividing(){
        while(true){
            yield return new WaitForSeconds(delayBetweenShots);
            
            Instantiate(DividedOrb , firePoint.position , firePoint.rotation);
            Instantiate(DividedOrb , firePoint.position ,
            Quaternion.Euler(firePoint.eulerAngles.x , firePoint.eulerAngles.y , firePoint.eulerAngles.z + 180f));

            //yield return new WaitForSeconds(delayBetweenShots);
        }
    }

    void FixedUpdate(){
        Rotation();
    }

    void Rotation(){
        transform.Rotate(0f , 0f , rotatingSpeed * Time.fixedDeltaTime);
    }
}
