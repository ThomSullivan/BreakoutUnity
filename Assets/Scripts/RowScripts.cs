using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowScripts : MonoBehaviour
{
    private GameObject brick;
    private int powerUpBrickIndex;
    void Start()
    {
        //Pick a random num between 0 and the number of bricks in this row
        powerUpBrickIndex = Random.Range(0, gameObject.transform.childCount);
        //get the gameObject of the the child brick at the chosen random index
        brick = gameObject.transform.GetChild(powerUpBrickIndex).gameObject;
        //Set the power up boolean to true, this causes the brick to spawn a power up on destruction.
        brick.GetComponent<BrickScripts>().hasPowerUp = true;
    }

  
}
