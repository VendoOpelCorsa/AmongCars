using UnityEngine;
 using System.Collections;
 
 public class PcInteractionSalida : MonoBehaviour {
    
     public Transform teleportTarget;
     public GameObject player;

     void OnTriggerEnter(Collider other){
        player.transform.position = teleportTarget.transform.position;
        if (!GameObject.FindWithTag("SonidoAmbiente").GetComponent<AudioSource>().isPlaying){
            GameObject.FindWithTag("CancionLaberinto").GetComponent<AudioSource>().Stop();
            GameObject.FindWithTag("SonidoAmbiente").GetComponent<AudioSource>().Play();
        }
        GameObject.FindWithTag("Transicion").GetComponent<AudioSource>().Play();
        player.SendMessage("recuperarCordura");
     }

 
 }
