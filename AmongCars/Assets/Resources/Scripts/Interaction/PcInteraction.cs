using UnityEngine;
 using System.Collections;
 
 public class PcInteraction : MonoBehaviour {
    
     public Transform teleportTarget;
     public GameObject player;

     void OnTriggerEnter(Collider other){
         player.transform.position = teleportTarget.transform.position;
     }

 
 }
