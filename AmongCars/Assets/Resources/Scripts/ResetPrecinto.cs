using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPrecinto : MonoBehaviour
{
    public GameObject cableRojo;
    public GameObject cableAzul;
    public GameObject cableVerde;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            cableRojo.transform.position = new Vector3(57.68f,2.58f,58.63f);
            cableVerde.transform.position = new Vector3(57.52f,2.57f,58.70f);
            cableAzul.transform.position = new Vector3(57.45f,2.4f,57.85f);
        }
    }
}
