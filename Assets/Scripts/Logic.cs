//This is a list of Unity modules in use within this script. 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Logic is a GameObject in code and Unity.
public class Logic : MonoBehaviour
{
    //A variable cannot be assigned or used until it is declared.
    //Decatring a variable requires:
        // a scope ( private or public)
        // a type (int, float, bool, GameObject, void)
        // a variable name, this is the name you will use in the script.
        // **optional** variable values can be assigned at declaration with the inclution of an = and corresponding type.  
    //Declartion and assignment of variables tied to game mechanics.
    //Creating public variables inside a GameObject allows access from within Unity.
    public int ballCount = 3;
    public int score = 0;
    public int streak = 0;
    public int multiplier = 1;
    public int freeBallCounter = 0;
    public int level = 1;
    public bool isPlaying = true;


    //Declaration of references that will be used, set as public and must be filled within Unity.
    //Unity script objects
    //BSS is the GameObject BallSpawnScript and and contains public voids, like SpawnBall().
    public BallSpawnerScript BSS;
    //GSS is the GameObject GameBoardSpawnerScript and contains public voids, like spawnGameBoard() 
    public GameBoardSpawnerScript GSS;
    public PlayerScript playerScript;
    public AudioSource audioPlayer;
    //GameObjects 
    public GameObject winScreen, betweenLevelFireworks, gameOverScreen, multiplierAnimation, freeBallAnimation;
    //If declaring multiple variables of the same type, they can be added after the initial declaration and seperated by a comma. 
    //User interface objects 
    public Text lives, scoreAmount, multiplierAmount, streakAmount;
    public TextMeshProUGUI levelAmount, finalScore;
    

    //The following methods are lifecycle methods that are a part of Unity.
    //Each represents a specific stage in the lifecycle of a GameObject.
    //Unity will run the contained code when the GameObject is in the corresponding lifecycle stage.
    
    //This function is always called before any Start functions and also just after a prefab is instantiated.
    void Awake()
    {
        //Mount the audio source logic. All sound is handled through the Audio source controlled by AudioSourceScripts.
        //audioPlayer = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSourceScripts>();
        //Code the begin the game. see below.
        startNewGame();
    }

  
    void Update()
    {
        //Code to monitor the number of bricks on screen. see below.
        checkGameBoard();
    }
   


    //The following are cutsom methods for handling different events in the game.
    //void is a way of decalring a method or function, this code can then be used over and over. It will not fire until called e.g. ballLoss();
    //If a public declartion is made the method is available outside of the current script via referencing the parent GameObject
    //startNewGame is used to begin a fresh game, it resets all game mechanic variables, updates the interface and then starts a level.
    public void startNewGame()
    {
        //Reset all game mechanic variables to the begining. 
        ballCount = 3;
        score = 0;
        streak = 0;
        multiplier = 1;
        freeBallCounter = 0;
        level = 1;
        //Update the interface.
        updateWholeUI();
        //Start a level.
        levelStart();

    }
        //levelStart is used to start any level. 
    public void levelStart()
    {
        //Hide a game over screen if coming from game over
        gameOverScreen.SetActive(false);
        //Hide the mid-level screen and decorations if coming from previous level
        winScreen.SetActive(false);
        betweenLevelFireworks.SetActive(false);
        //Update interface
        updateWholeUI();
        //Remove previous GameBoard 
        Destroy(GameObject.FindGameObjectWithTag("GameBoard"));
        //Spawn new GameBoard, GSS is the GameObject GameBoardSpawnerScript and contains the public void, spawnGameBoard() 
        GSS.spawnGameBoard();
        //BSS is the GameObject BallSpawnScript and SpawnBall() is a public void within it.
        BSS.SpawnBall();
        //Set isplaying back to true to reenable board monitoring by checkGameBoard()
        isPlaying = true;
    }
        //checkGameBoard is used to check for when the level is finished.
    private void checkGameBoard()
    {
        //GameObject.FindGameObjectsWithTag("Brick").Length <-- this returns the count of the current number of bricks in the scene.
        //GameObject.FindGameObjectWithTag("GameBoard") != null <-- this determines if a GameBoard object is present, this is needed to allow the removal of the GameBoard at the end of each level.
        // So if there are no bricks, there is a GameBoard and, isPlaying is true this code will execute.
        if(GameObject.FindGameObjectsWithTag("Brick").Length == 0 && GameObject.FindGameObjectWithTag("GameBoard") != null && isPlaying == true) 
        {
            //Change isPlaying to false. This prevents this code from executing once the GameBoard is destoryed by levelFinish();.
            isPlaying = false;
            //Code to end a level.
            levelFinish();
        }
    }
    //levelFinish is used to end a level it fires when checkGameBoard() triggers it.
    private void levelFinish()
    {
        //Remove the balls 
        foreach(GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
        {
            Destroy(ball);
        }
        foreach(GameObject powerUp in GameObject.FindGameObjectsWithTag("powerUp"))
        {
            Destroy(powerUp);
        }
        //Turn on the mid-level interface screen
        winScreen.SetActive(true);
        //turn on hidden fireworks 
        betweenLevelFireworks.SetActive(true);
        //Increase the level by one.
        level += 1;
    }
    //gameOver is used to end the current game, it fires when the final ball has made contact with the killzone.
    private void gameOver()
    {   
        //Remove the GameBoard from the screen
        Destroy(GameObject.FindGameObjectWithTag("GameBoard"));
        playerScript.disablePowerUps();
        //Set referenced interface GameObject text content.
        finalScore.text = score.ToString();
        //Enable the hidden GameObject gameOverScreen
        gameOverScreen.SetActive(true);
    }
        // ballLoss handles the logic need for losing a ball in the game
    public void ballLoss()
    {
        //Reduce the ballCount by 1
        ballCount -= 1;
        //Reset the streak
        streak = 0;
        //Update the interface.
        updateWholeUI();
        
        if (ballCount > 0)
        {
            //If the player have more that 1 life left spawn a ball.
            //BSS is the GameObject BallSpawnScript and SpawnBall() is a public void within it.
            BSS.SpawnBall();
        }
        else
        {
            //If the player has 0 balls trigger game over.
            isPlaying = false;
            gameOver();
        }
    }
    // addScore handles operations when the score is increased.
    //ContextMenu("name") adds a clickable button in Unity to fire any related code through the Unity interface at will.
    [ContextMenu("Increase Score")]
    public void addScore(int amount)
    {
        //Increment the player's score by one times the current value of multiplier.
        score += (amount * multiplier);
        //Increase the streak amount by one.
        streak += 1;
        //Increase the free ball counter, 1 free ball every 100 bricks broken. This value does not reset upon ball loss.
        freeBallCounter += 1;
        //Check to see if the player has earned a free ball.
        if (freeBallCounter > 99)
        {
            //Increase ball count by one.
            ballCount += 1;
            //Update interface.
            updateLivesCounter();
            freeBallAnimation.SetActive(true);
            audioPlayer.Play();
            //Reset freeBallCounter to 0.
            freeBallCounter = 0;
        }
        //Update interface.
        updateWholeUI();
        
    }
    private void setMultiplier()
    {
        if (streak < 10){
            //Less than 10
            multiplier = 1;
        }else if (streak < 20 && streak >= 10){
            //10-19
            if(multiplier == 1)
            {
                multiplierUp();
            }
            multiplier = 2 ;
        }else if (streak < 30 && streak >= 20){
            //20-29
            if(multiplier == 2)
            {
                multiplierUp();
            }
            multiplier = 3;
        }else{
            //30-
            if(multiplier == 3)
            {
                multiplierUp();
            }
            multiplier = 4;
        }
        updateMultiplier();
    }
    
    private void multiplierUp()
    {
        audioPlayer.Play();
        multiplierAnimation.SetActive(true);
        
    }
    //The following custom methods handle updating the text in the user interface.
    // UI UPDATING
    private void updateWholeUI()
    {
        setMultiplier();
        updateLivesCounter();
        updateStreakAmount();
        updateScoreAmount();
        updateLevelAmount();
    }
    private void updateScoreAmount()
    {
        //Set referenced interface GameObject text content.
        scoreAmount.text = score.ToString();
    }
    private void updateLivesCounter()
    {
        //Set referenced interface GameObject text content.
        lives.text = ballCount.ToString();
    }
    private void updateStreakAmount()
    {
        //Set referenced interface GameObject text content.
        streakAmount.text = streak.ToString();
    }
    private void updateMultiplier() 
    {
        
        //Set referenced interface GameObject text content.
        multiplierAmount.text = multiplier.ToString();
    }
    private void updateLevelAmount()
    {
        //Set referenced interface GameObject text content.
        levelAmount.text = level.ToString();
    }
    // END UI UPDATING
}
