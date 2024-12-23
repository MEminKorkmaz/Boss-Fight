using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UpgradeManager : MonoBehaviour
{
    public Text MediumTxt;
    public Text High1Txt;
    public Text High2Txt;
    public Text High3Txt;
    public Text Low1Txt;
    public Text Low2Txt;
    public Text NothingTxt;

    public GameObject NextButton;

    public Button MaxHealthButton;

    public GameObject DicePrefab;
    public GameObject[] DiceSides;
    public GameObject DiceButton;

    public Image[] DiceImages;
    
    private int DiceValue;

    public int nextLevel;

    public PlayerManager playerManager;

    public GameManager gameManager;



    void Awake(){
        playerManager = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>();

        gameManager = GetComponent<GameManager>();

        NextButton.SetActive(false);
    }


    void Start(){
        if(nextLevel == 2)
        NextLevel2();

        else if(nextLevel == 3)
        NextLevel3();

        else if(nextLevel == 4)
        NextLevel4();
    }

    public void NextLevel(){
        gameManager.isLevelEnded = false;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MaxHealthIncrease(){
        playerManager.lives++;

        MaxHealthButton.interactable = false;

        DiceButton.SetActive(false);
        NextButton.SetActive(true);
    }

    void NextLevel2(){
        MediumTxt.text = "+1 MAX HEALTH";
        High1Txt.text = "%15 CRIT CHANCE";
        High2Txt.text = "+10 DAMAGE";
        High3Txt.text = "DOUBLE BULLET";
        Low1Txt.text = "%5 CRIT CHANCE";
        Low2Txt.text = "+5 DAMAGE";
        NothingTxt.text = "NOTHING";
    }

    void NextLevel3(){
        MediumTxt.text = "+1 MAX HEALTH";
        High1Txt.text = "%20 CRIT CHANCE";
        High2Txt.text = "+15 DAMAGE";
        High3Txt.text = "TRIPLE BULLET";
        Low1Txt.text = "%5 CRIT CHANCE";
        Low2Txt.text = "+5 DAMAGE";
        NothingTxt.text = "NOTHING";
    }

    void NextLevel4(){
        MediumTxt.text = "+1 MAX HEALTH";
        High1Txt.text = "%25 CRIT CHANCE";
        High2Txt.text = "+20 DAMAGE";
        High3Txt.text = "HOMING BULLET";
        Low1Txt.text = "%5 CRIT CHANCE";
        Low2Txt.text = "+5 DAMAGE";
        NothingTxt.text = "NOTHING";
    }

    private GameObject tempDice;
    public void RollTheDice(){
        MaxHealthButton.interactable = false;
        DiceButton.SetActive(false);
        Vector3 Target = new Vector3(0 , -4f , 0f);
        tempDice = Instantiate(DicePrefab , Target , Quaternion.identity);
        Invoke(nameof(DiceSideFunc) , 0.5f);
    }

    void DiceSideFunc(){
        tempDice.SetActive(false);

        Vector3 Target = new Vector3(0 , -4f , 0f);

        int rnd = Random.Range(0 , 5);
        tempDice = Instantiate(DiceSides[rnd] , Target , Quaternion.identity);
        DiceImages[rnd].color = Color.red;
        DiceValue = rnd + 1;
        UpgradePlayer();
    }

    void UpgradePlayer(){
        if(nextLevel == 2){
            if(DiceValue == 1){
                playerManager.critChance += 15;
            }

            else if(DiceValue == 2){
                playerManager.bonusDamage += 10;
            }

            else if(DiceValue == 3){
                playerManager.isDoubleBullet = true;
                playerManager.isTripleBullet = false;
                playerManager.isHomingBullet = false;
            }

            else if(DiceValue == 4){
                playerManager.critChance += 5;
            }

            else if(DiceValue == 5){
                playerManager.bonusDamage += 5;
            }
        }

        else if(nextLevel == 3){
            if(DiceValue == 1){
                playerManager.critChance += 20;
            }

            else if(DiceValue == 2){
                playerManager.bonusDamage += 15;
            }

            else if(DiceValue == 3){
                playerManager.isDoubleBullet = false;
                playerManager.isTripleBullet = true;
                playerManager.isHomingBullet = false;
            }

            else if(DiceValue == 4){
                playerManager.critChance += 5;
            }

            else if(DiceValue == 5){
                playerManager.bonusDamage += 5;
            }
        }

        else if(nextLevel == 4){
            if(DiceValue == 1){
                playerManager.critChance += 25;
            }

            else if(DiceValue == 2){
                playerManager.bonusDamage += 25;
            }

            else if(DiceValue == 3){
                playerManager.isDoubleBullet = false;
                playerManager.isTripleBullet = false;
                playerManager.isHomingBullet = true;
            }

            else if(DiceValue == 4){
                playerManager.critChance += 5;
            }

            else if(DiceValue == 5){
                playerManager.bonusDamage += 5;
            }
        }

        NextButton.SetActive(true);
    }
}
