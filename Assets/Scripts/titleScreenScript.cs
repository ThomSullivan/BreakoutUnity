using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleScreenScript : MonoBehaviour
{
    public titleScreenSpawner spawner1, spawner2, spawner3, spawner4;
    private int ballcount = 0;
    private float timer = 3;
    private void Update() {
        if (timer > 3)
        {
            if (ballcount <26)
            {
                spawner1.SpawnBall();
                spawner2.SpawnBall();
                spawner3.SpawnBall();
                spawner4.SpawnBall();
                ballcount += 1;
            }
            timer = 0;
        } 
        timer += (1 * Time.deltaTime);
    }
    public void startGame()
    {  
        SceneManager.LoadScene("gameScreen");
    }

}
