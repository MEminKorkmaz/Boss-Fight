using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterOrb : MonoBehaviour
{
    public Rigidbody2D rb;

    public GameObject DividedOrb;

    public Transform firePoint;

    public float speed;

    public float delayBetweenShots;
    public float destroyTime;

    public float delayTime;



    void Start()
    {
        Invoke(nameof(DelayFunc) , delayTime);
        Destroy(gameObject , destroyTime);
    }

    void DelayFunc(){
        StartCoroutine("Dividing");
    }


    IEnumerator Dividing(){
        while(true){
            yield return new WaitForSeconds(delayBetweenShots);
            
            /*Instantiate(DividedOrb , firePoint.position , firePoint.rotation);
            Instantiate(DividedOrb , firePoint.position ,
            Quaternion.Euler(firePoint.eulerAngles.x , firePoint.eulerAngles.y , firePoint.eulerAngles.z + 90f));
            Instantiate(DividedOrb , firePoint.position ,
            Quaternion.Euler(firePoint.eulerAngles.x , firePoint.eulerAngles.y , firePoint.eulerAngles.z + 180f));
            Instantiate(DividedOrb , firePoint.position ,
            Quaternion.Euler(firePoint.eulerAngles.x , firePoint.eulerAngles.y , firePoint.eulerAngles.z + 270f));*/

            //yield return new WaitForSeconds(delayBetweenShots);

            Instantiate(DividedOrb , firePoint.position ,
            Quaternion.Euler(firePoint.eulerAngles.x , firePoint.eulerAngles.y , firePoint.eulerAngles.z + 45f));
            Instantiate(DividedOrb , firePoint.position ,
            Quaternion.Euler(firePoint.eulerAngles.x , firePoint.eulerAngles.y , firePoint.eulerAngles.z + 135f));
            Instantiate(DividedOrb , firePoint.position ,
            Quaternion.Euler(firePoint.eulerAngles.x , firePoint.eulerAngles.y , firePoint.eulerAngles.z + 225f));
            Instantiate(DividedOrb , firePoint.position ,
            Quaternion.Euler(firePoint.eulerAngles.x , firePoint.eulerAngles.y , firePoint.eulerAngles.z + 315f));

            //yield return new WaitForSeconds(delayBetweenShots);
        }
    }

    void FixedUpdate(){
        Move();
    }

    void Move(){
        rb.velocity = transform.up * speed * Time.fixedDeltaTime;
    }
}
