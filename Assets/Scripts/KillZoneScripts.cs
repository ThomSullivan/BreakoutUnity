using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneScripts : MonoBehaviour
{
    public Logic logic;
    private int activeBallCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        activeBallCount = GameObject.FindGameObjectsWithTag("Ball").Length;
        //Debug.Log(activeBallCount);
        if (activeBallCount == 0 && logic.isPlaying)
        {
            logic.ballLoss();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        Destroy(collision.gameObject);
    }
}
