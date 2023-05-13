using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScripts : MonoBehaviour
{
    public Logic logic;
    public GameObject powerUp;
    public bool hasPowerUp = false;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (hasPowerUp)
        {
            spawnPowerUp();
        }
        logic.addScore(1);
        Destroy(gameObject);
    }
    public void spawnPowerUp()
    {
        Instantiate(powerUp, transform.position, transform.rotation);
    }
}
