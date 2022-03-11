using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D Trap) 
   {
       if(Trap.CompareTag("Player"))
       {
           Destroy(gameObject);
       }
   }
}
