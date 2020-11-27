using UnityEngine;
 using System.Collections;
 
 public class PcInteraction : MonoBehaviour {
    
     public Transform teleportTarget;
     public GameObject player;

     void OnTriggerEnter(Collider other){
         player.transform.position = teleportTarget.transform.position;
        if (!GameObject.FindWithTag("CancionLaberinto").GetComponent<AudioSource>().isPlaying){
            GameObject.FindWithTag("CancionLaberinto").GetComponent<AudioSource>().Play();
            GameObject.FindWithTag("SonidoAmbiente").GetComponent<AudioSource>().Stop();
        }
        GameObject.FindWithTag("SonidoTP").GetComponent<AudioSource>().Play();
     }

 
 }
