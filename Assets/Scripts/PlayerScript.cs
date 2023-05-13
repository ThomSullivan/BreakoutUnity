using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
   private int powerUpNumber;
   private Vector3 playerNormalSize;
   public bool isLaserActive = false;

   public BallSpawnerScript ballSpawner;
   public PlayerContainerScripts playerContainerScripts;
   public Logic logic;
   public Sprite  watermelon, ballSprite;
   public GameObject bumper, laserAnimation;
   public Rigidbody2D laser;
   

   private GameObject ball;
   private SpriteRenderer ballRender;
   private CircleCollider2D ballCollider;

   private void Awake() 
   {
     playerNormalSize = transform.localScale;
   }
     private void Update() 
     {
          if(!isLaserActive)
          {
               laserAnimation.SetActive(false);
          }
     }


    
    public void disablePowerUps()
    {
     isLaserActive = false;
     bumper.SetActive(false);
     gameObject.transform.localScale = playerNormalSize;
    }
    
    public void activatePowerUp()
    {
        powerUpNumber = Random.Range(1,7);
        switch(powerUpNumber)
        {
          case 1:
               powerUpMultiBall();
               break;
          case 2:
               powerUpExtendPlayer();
               break;
          case 3:
               powerUpWatermelon();
               break;
          case 4:
               powerUpBumper();
               break;
          case 5:
               powerUpRetractPlayer();
               break;
          case 6:
               powerUpLaser();
               break;
            default:
               break;
        }
    }
    private void powerUpExtraPoints()
    {
          logic.addScore(5);
          playerContainerScripts.showExtraPointAnimnation();
          
    }
    private void powerUpMultiBall()
    {
          playerContainerScripts.showMultiBallAnimation();
          ballSpawner.SpawnBall();
    }
    private void powerUpBumper()
    {
          playerContainerScripts.showBumperAnimation();
          bumper.SetActive(true);
    }
    [ContextMenu("activatePowerUp()")]
    private void powerUpLaser()
    {
     
          isLaserActive = true;
          laserAnimation.SetActive(true);
    }
    public void powerUpFireLaser()
    {
     Rigidbody2D clone;
     clone = Instantiate(laser, transform.position, transform.rotation);
     clone.velocity = transform.TransformDirection(Vector2.up * 100);
    }
    private void powerUpExtendPlayer()
    {
     //only extend if player size is normal
     if (transform.localScale.x == playerNormalSize.x)
     {
          playerContainerScripts.showExtendAnimation();
          //extend the player
          gameObject.transform.localScale += new Vector3(3,0,0);
          //fire coroutine to shut off extend 
          StartCoroutine(DisablePlayerExtend());
     }else{
          powerUpExtraPoints();
     }
    }
    IEnumerator DisablePlayerExtend()
    {
     //wait 15 seconds
      yield return new WaitForSeconds(15);
      //shrink player back
      gameObject.transform.localScale = playerNormalSize;
    }
    private void powerUpRetractPlayer()
    {
     //only extend if player size is normal
     if (transform.localScale.x >= playerNormalSize.x)
     {
          playerContainerScripts.showShrunkAnimation();
          //extend the player
          gameObject.transform.localScale -= new Vector3(2,0,0);
          //fire coroutine to shut off extend 
          StartCoroutine(DisablePlayerRetract());
     }else{
          powerUpExtraPoints();
     }
    }
    IEnumerator DisablePlayerRetract()
    {
     //wait 15 seconds
      yield return new WaitForSeconds(15);
      //shrink player back
      gameObject.transform.localScale = playerNormalSize;
    }
    private void powerUpWatermelon()
    {
     ball = GameObject.FindGameObjectWithTag("Ball");
     if (ball.transform.localScale.x == 0.05f)
     {
          playerContainerScripts.showWatermelonAnimation();
          ball.transform.localScale += new Vector3(0.2f,0.2f,0.2f);
          ballRender = ball.GetComponent<SpriteRenderer>();
          ballRender.sprite = watermelon;
          StartCoroutine(DisableWatermelon());
     }else{
          powerUpExtraPoints();
     }
    }
    IEnumerator DisableWatermelon()
    {
     yield return new WaitForSeconds(15);
     ball.transform.localScale -= new Vector3(0.2f,0.2f,0.2f);
     ballRender.sprite = ballSprite;
    }

}
