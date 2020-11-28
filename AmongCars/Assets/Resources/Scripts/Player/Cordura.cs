using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cordura : MonoBehaviour
{   
    public float cordura, corduraTotal = 100f;

    public Image textoCordura;


    // Start is called before the first frame update
    void Start()
    {
        cordura = corduraTotal;
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    void quitarCordura(){
        cordura = Mathf.Clamp(cordura - (corduraTotal/3), 0f, corduraTotal);
        print(cordura);
        textoCordura.transform.localScale = new Vector2( cordura/corduraTotal, 1);
    }

    float getCordura(){
        return cordura;
    }
}
