using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedInteraction : Interactive
{
    public RedInteraction esferaComplementaria;
    public GameObject cable;

    public bool Found { private set; get; }


    public override void OnInteract()
    {
        Found = true;
        print("encontrado");
        if(esferaComplementaria.Found){
            print("cable movido");
            if(cable.CompareTag("cableRojo")){
                cable.transform.position = new Vector3(56.07f,2.47f,58.62f);
            }
            if(cable.CompareTag("cableVerde")){
                cable.transform.position = new Vector3(56.04f,2.48f,58.67f);
            }
            if(cable.CompareTag("cableAzul")){
                cable.transform.position = new Vector3(56.14f,2.43f,57.75f);
            }
            //Found = false;
        }
        
    }
}
