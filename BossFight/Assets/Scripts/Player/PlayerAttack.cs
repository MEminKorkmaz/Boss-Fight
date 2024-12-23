using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject ProjectilePrefab;

    public Transform firePoint;

    public float fireRate;

    public float attackSpeedMultiplier;

    //private float tempFireRate;
    private bool canAttack;

    public float bulletSpread;

    public PlayerManager playerManager;

    private bool isDoubleBullet;
    private bool isTripleBullet; 





    void Start(){
        //tempFireRate = 0f;

        playerManager = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>();

        fireRate = playerManager.fireRate;
        canAttack = true;

        isDoubleBullet = playerManager.isDoubleBullet;
        isTripleBullet = playerManager.isTripleBullet;
    }

    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)
        || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)){
            Attack();
        }*/
        if(Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.Space)){
            Attack();
        }

        isDoubleBullet = playerManager.isDoubleBullet;
        isTripleBullet = playerManager.isTripleBullet;
    }

    void Attack(){
        //tempFireRate -= Time.deltaTime;

        //if(tempFireRate <= 0f){
            if(!canAttack) return;

            if(isDoubleBullet){
            Vector3 tempRightBullet = new Vector3(0.1f , 0f , 0f);
            Instantiate(ProjectilePrefab , firePoint.position + tempRightBullet , firePoint.rotation);

            Vector3 tempLeftBullet = new Vector3(-0.1f , 0f , 0f);
            Instantiate(ProjectilePrefab , firePoint.position + tempLeftBullet , firePoint.rotation);
            }

            else if(isTripleBullet){
            Instantiate(ProjectilePrefab , firePoint.position , Quaternion.Euler(firePoint.eulerAngles.x , 
            firePoint.eulerAngles.y ,
            firePoint.eulerAngles.z - 10f));

            Instantiate(ProjectilePrefab , firePoint.position , firePoint.rotation);

            Instantiate(ProjectilePrefab , firePoint.position , Quaternion.Euler(firePoint.eulerAngles.x , 
            firePoint.eulerAngles.y ,
            firePoint.eulerAngles.z + 10f));
            }

            else{
            float x = Random.Range(-bulletSpread , bulletSpread);
            Instantiate(ProjectilePrefab , firePoint.position , Quaternion.Euler(firePoint.eulerAngles.x , 
            firePoint.eulerAngles.y ,
            firePoint.eulerAngles.z + x));
            }

            //tempFireRate = fireRate;

            canAttack = false;
            Invoke(nameof(isAttackFunc) , fireRate);
        //}
    }

    void isAttackFunc(){
        canAttack = true;
    }
}
