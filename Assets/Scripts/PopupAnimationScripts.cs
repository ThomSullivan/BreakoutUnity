using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupAnimationScripts : MonoBehaviour
{
    private float animationTimer = 3;
 
    
    private void OnEnable() {
        animationTimer = 3;
    }
    void Update()
    {
        animationTimer -= (1 * Time.deltaTime);
        if (animationTimer < 1)
        {
            this.gameObject.SetActive(false);
        }
    }
}
