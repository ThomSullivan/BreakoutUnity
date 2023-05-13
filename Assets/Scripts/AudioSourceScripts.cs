using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceScripts : MonoBehaviour
{
    public AudioClip blip, rockDestroy, powerUp;
    public AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    public void playBlip()
    {
        src.clip = blip;
        src.Play();
    }
    public void playRockDestroy()
    {
        src.clip = rockDestroy;
        src.Play();
    }
    public void multiplierUpSound()
    {
        src.clip = powerUp;
        src.Play();
    }
    
}
