using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContainerScripts : MonoBehaviour
{
    public PlayerScript playerScript;
    public GameObject bumperAnimation, watermelonAnimation, multiBallAnimation, shrunkAnimation, extendAnimation, extraPointAnimation;
    private float moveSpeed = 45;
    public AudioSource src;
    
        // Update is called once per frame
    void Update()
    {
        //Keyboard movement
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > -13)
            {
                transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < 13)
            {
                transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
            }
        }
        // Mouse and touch controls
        if(Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            if (mousePosition.x < 13 && mousePosition.x > -13)
            {
                transform.position = new Vector3(mousePosition.x, transform.position.y, transform.position.z);
            }
        }
        //Laser managment
        if(Input.GetMouseButtonDown(0)){
            if(playerScript.isLaserActive)
            {
                playerScript.powerUpFireLaser();
                src.Play();
                playerScript.isLaserActive = false;
            }
        }

    }
    public void showBumperAnimation()
    {
        bumperAnimation.SetActive(true);
    }
    public void showWatermelonAnimation()
    {
        watermelonAnimation.SetActive(true);
    }
    public void showMultiBallAnimation()
    {
        multiBallAnimation.SetActive(true);
    }
    public void showShrunkAnimation()
    {
        shrunkAnimation.SetActive(true);
    }
    public void showExtendAnimation()
    {
        extendAnimation.SetActive(true);
    }
    public void showExtraPointAnimnation()
    {
        extraPointAnimation.SetActive(true);
    }
}
