using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public KeyInteraction key;
    public GameObject puerta;

    void OnTriggerEnter(){
        
        if(key.Found){
            puerta.SetActive(false);
        }
    }
    
}
