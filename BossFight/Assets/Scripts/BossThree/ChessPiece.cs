using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    public Transform Target;

    private Vector3 tempTarget;
    
    private Vector3 initialPosition;

    public int pieceValue;// 0 - Rook ** 1 - Bishop ** 2 - Knight ** 3 - Queen

    public float speed;

    public float moveDelay;

    public float minTargetTimer;
    public float maxTargetTimer;
    private float findTargetTimer;


    
    void Start()
    {
        Target = GameObject.FindWithTag("player").transform;

        tempTarget = new Vector3(transform.position.x , transform.position.y , 0f);

        initialPosition = new Vector3(transform.position.x , transform.position.y , transform.position.z);

        InvokeRepeating(nameof(TargetIsInitialPos) , 15f , 15f);
    }

    void TargetIsInitialPos(){
        tempTarget = initialPosition;
    }


    void Update()
    {
        //CheckForDistance();
        
        transform.position = Vector3.MoveTowards(transform.position , tempTarget , speed * Time.deltaTime);
        
        float distance = Vector3.Distance(transform.position , Target.position);
        if(distance <= 1f)
        return;
        //transform.position = Vector3.MoveTowards(transform.position , tempTarget , speed * Time.deltaTime);
        CanSeeThePlayer();
        //TargetForIdle();
    }

    private Vector3 tempTarget2;
    void CanSeeThePlayer(){
        RaycastHit2D[] hits;
        
        hits = Physics2D.RaycastAll(transform.position, Target.position - transform.position);

        foreach (var hit in hits)
        {
            if(hit.collider.gameObject.tag == "player"){
                if(pieceValue == 0){
                    if(Target.position.x == transform.position.x){
                        tempTarget2 = new Vector3(Target.position.x , Target.position.y , 0f);
                        Invoke(nameof(Move) , moveDelay);
                    }
                    if(Target.position.y == transform.position.y){
                        tempTarget2 = new Vector3(Target.position.x , Target.position.y , 0f);
                        Invoke(nameof(Move) , moveDelay);
                    }
                }

                if(pieceValue == 1){
                    float distance = Mathf.Abs(transform.position.x - Target.position.x);
                    float distance2 = Mathf.Abs(transform.position.y - Target.position.y);

                    if(Mathf.Abs(distance) == Mathf.Abs(distance2)){
                        tempTarget2 = new Vector3(Target.position.x , Target.position.y , 0f);
                        Invoke(nameof(Move) , moveDelay);
                    }
                    /*if(Target.position.y == transform.position.y){
                        tempTarget2 = new Vector3(Target.position.x , Target.position.y , 0f);
                        Invoke(nameof(Move) , moveDelay);
                    }*/
                }

                if(pieceValue == 2){
                    if(Target.position.x == transform.position.x + 1 && Target.position.y == transform.position.y - 2){
                        tempTarget2 = new Vector3(Target.position.x , Target.position.y , 0f);
                        Invoke(nameof(Move) , moveDelay);
                    }

                    if(Target.position.x == transform.position.x - 1 && Target.position.y == transform.position.y - 2){
                        tempTarget2 = new Vector3(Target.position.x , Target.position.y , 0f);
                        Invoke(nameof(Move) , moveDelay);
                    }

                    if(Target.position.x == transform.position.x - 2 && Target.position.y == transform.position.y - 1){
                        tempTarget2 = new Vector3(Target.position.x , Target.position.y , 0f);
                        Invoke(nameof(Move) , moveDelay);
                    }

                    if(Target.position.x == transform.position.x - 2 && Target.position.y == transform.position.y + 1){
                        tempTarget2 = new Vector3(Target.position.x , Target.position.y , 0f);
                        Invoke(nameof(Move) , moveDelay);
                    }

                    if(Target.position.x == transform.position.x - 1 && Target.position.y == transform.position.y + 2){
                        tempTarget2 = new Vector3(Target.position.x , Target.position.y , 0f);
                        Invoke(nameof(Move) , moveDelay);
                    }

                    if(Target.position.x == transform.position.x + 1 && Target.position.y == transform.position.y + 2){
                        tempTarget2 = new Vector3(Target.position.x , Target.position.y , 0f);
                        Invoke(nameof(Move) , moveDelay);
                    }

                    if(Target.position.x == transform.position.x + 2 && Target.position.y == transform.position.y + 1){
                        tempTarget2 = new Vector3(Target.position.x , Target.position.y , 0f);
                        Invoke(nameof(Move) , moveDelay);
                    }

                    if(Target.position.x == transform.position.x + 2 && Target.position.y == transform.position.y - 1){
                        tempTarget2 = new Vector3(Target.position.x , Target.position.y , 0f);
                        Invoke(nameof(Move) , moveDelay);
                    }

                    /*if(Target.position.x == transform.position.x + 1 && Target.position.y == transform.position.y + 2){
                        tempTarget2 = new Vector3(Target.position.x , Target.position.y , 0f);
                        Invoke(nameof(Move) , moveDelay);
                    }

                    if(Target.position.x - 1 == transform.position.x && Target.position.y - 2 == transform.position.y){
                        tempTarget2 = new Vector3(Target.position.x , Target.position.y , 0f);
                        Invoke(nameof(Move) , moveDelay);
                    }

                    if(Target.position.x == transform.position.x - 1 && Target.position.y == transform.position.y - 2){
                        tempTarget2 = new Vector3(Target.position.x , Target.position.y , 0f);
                        Invoke(nameof(Move) , moveDelay);
                    }*/
                }

                if(pieceValue == 3){
                    if(Target.position.x == transform.position.x){
                        tempTarget2 = new Vector3(Target.position.x , Target.position.y , 0f);
                        Invoke(nameof(Move) , moveDelay);
                    }
                    if(Target.position.y == transform.position.y){
                        tempTarget2 = new Vector3(Target.position.x , Target.position.y , 0f);
                        Invoke(nameof(Move) , moveDelay);
                    }

                    float distance = Mathf.Abs(transform.position.x - Target.position.x);
                    float distance2 = Mathf.Abs(transform.position.y - Target.position.y);

                    if(Mathf.Abs(distance) == Mathf.Abs(distance2)){
                        tempTarget2 = new Vector3(Target.position.x , Target.position.y , 0f);
                        Invoke(nameof(Move) , moveDelay);
                    }
                }
            }
        }
    }

    void Move(){
        //transform.position = Vector3.MoveTowards(transform.position , tempTarget , speed * Time.deltaTime);
        tempTarget = tempTarget2;
    }

    void CheckForDistance(){
        float distance = Vector3.Distance(transform.position , Target.position);
        if(distance <= 10f)
        return;
    }

    void TargetForIdle(){
        findTargetTimer -= Time.deltaTime;

        if(findTargetTimer > 0f) return;
            float x = Random.Range(-5 , 5);
            float y = Random.Range(-5 , 0);

            x += 0.5f;
            y += 0.5f;

            Mathf.RoundToInt(x);
            Mathf.RoundToInt(y);
            
            Vector3 SpawnPos = new Vector3(x , y , 0f);

            tempTarget = SpawnPos;

            findTargetTimer = Random.Range(minTargetTimer , maxTargetTimer);
            Debug.Log(findTargetTimer);
    }
}
