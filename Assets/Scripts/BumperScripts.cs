using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperScripts : MonoBehaviour
{
   private void OnCollisionEnter2D(Collision2D other)
   {
    if(other.gameObject.tag =="Ball")
    {
        gameObject.SetActive(false);
    }
   }
}
