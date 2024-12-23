using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public Transform movePoint;

    public float speed;

    private bool canMove;

    public GameManager gameManager;




    void Start(){
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        //tempMoveTime = moveTime;
        movePoint.parent = null;
    }

    // TEMPORARY

    public void up(){
        transform.rotation = Quaternion.Euler(0f , 0f , 0f);

                if(transform.position.y >= 0.5f) return;
                movePoint.position += new Vector3(0f , 1f , 0f);
    }

    public void down(){
        transform.rotation = Quaternion.Euler(0f , 0f , 180f);

                if(transform.position.y <= -4.5f) return;
                movePoint.position += new Vector3(0f , -1f , 0f);
    }

    public void right(){
        transform.rotation = Quaternion.Euler(0f , 0f , -90f);

                if(transform.position.x >= 4.5f) return;
                movePoint.position += new Vector3(1f , 0f , 0f);
    }

    public void left(){
        transform.rotation = Quaternion.Euler(0f , 0f , 90f);

                if(transform.position.x <= -4.5f) return;
                movePoint.position += new Vector3(-1f , 0f , 0f);
    }
    // TEMPORARY


    void Update(){
        if(gameManager.isGameOver || gameManager.isLevelEnded) return;
        
        transform.position = Vector2.MoveTowards(transform.position , movePoint.position , speed * Time.deltaTime);
        //tempMoveTime -= Time.deltaTime;
        if(canMove == true){
            if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
                transform.rotation = Quaternion.Euler(0f , 0f , 90f);

                if(transform.position.x <= -4.5f) return;
                movePoint.position += new Vector3(-1f , 0f , 0f);

            }
            else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
                transform.rotation = Quaternion.Euler(0f , 0f , -90f);

                if(transform.position.x >= 4.5f) return;
                movePoint.position += new Vector3(1f , 0f , 0f);

            }
            else if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
                transform.rotation = Quaternion.Euler(0f , 0f , 0f);

                if(transform.position.y >= 0.5f) return;
                movePoint.position += new Vector3(0f , 1f , 0f);

            }
            else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
                transform.rotation = Quaternion.Euler(0f , 0f , 180f);

                if(transform.position.y <= -4.5f) return;
                movePoint.position += new Vector3(0f , -1f , 0f);
                
            }
        }
        float distance = Vector2.Distance(transform.position , movePoint.position);
        if(distance <= 0.4f){
            canMove = true;
        }
        else{
            canMove = false;
        }
    }
}
