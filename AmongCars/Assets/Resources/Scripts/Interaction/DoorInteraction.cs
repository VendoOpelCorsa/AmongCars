using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : ObjectInteraction
{
    public KeyInteraction key;
   public override void OnInteract()
    {
        base.OnInteract();

        if(key.Found){
            gameObject.SetActive(false);
        }
        
    }
}
