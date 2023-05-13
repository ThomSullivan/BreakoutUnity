using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScripts : MonoBehaviour
{
    private AudioSourceScripts audioPlayer;

    void Awake()
    {
        //The ball is a prefab so the reference cannot be filled through Unity.
        audioPlayer = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSourceScripts>();
    }
    
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.name == "Brick")
        {
            audioPlayer.playRockDestroy();
        }else{
            audioPlayer.playBlip();
        }
    }
    
}
