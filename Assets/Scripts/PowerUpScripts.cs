using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScripts : MonoBehaviour
{
    //private Logic logic;
    
    private PlayerScript player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        //ballSpawner = GameObject.FindGameObjectWithTag("BallSpawner").GetComponent<BallSpawnerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        
        if ( collision.gameObject.layer == 3 )
        {   
           
           
           Debug.Log("POWERUP ACTIVATED!!");
           player.activatePowerUp();
           Destroy(gameObject);
           
        }
        
    }
    
    
}
