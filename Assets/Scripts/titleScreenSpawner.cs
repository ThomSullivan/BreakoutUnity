using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleScreenSpawner : MonoBehaviour
{
    //Make a reference to the ball object. In this case a Prefab, this reference must be filled within Unity.
    public Rigidbody2D Ball;
    //Make a reference to the Logic game object to utilize the level amount to increase ball speed, this reference must be filled within Unity.
    


    //void is a way of decalring a method or function, this code can then be used over and over. It will not fire until called e.g. SpawnBall();
    //If a public declartion is made the method is available outside of the current script via referencing the parent GameObject(see BallSpawnerScript BSS declaration in Logic.cs).
    public void SpawnBall()
        //The SpawnBall method is a custom method, it spawns a ball into the game and applies velocity to it. 
    {
        //Create a new Ball in the game world.
        Rigidbody2D clone; 
        clone = Instantiate(Ball, transform.position, transform.rotation);
        //Hit the cloned ball in the up direction realative to the spawner
        clone.velocity = transform.TransformDirection(Vector2.up * 50);// Logarithmic function for increase in ball speed allows for non-linear growth of ball speed each level.
    }
}
