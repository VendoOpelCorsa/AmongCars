using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cordura : MonoBehaviour
{   
    public float cordura, corduraTotal = 100f;

    public Image textoCordura;

    void Start()
    {
        cordura = corduraTotal;
    }

    void quitarCordura(){
        cordura = Mathf.Clamp(cordura - (corduraTotal/3), 0f, corduraTotal);
        print(cordura);
        textoCordura.transform.localScale = new Vector2( cordura/corduraTotal, 1);
    }

    void recuperarCordura(){
        cordura = corduraTotal;
        print(cordura);
        textoCordura.transform.localScale = new Vector2( cordura/corduraTotal, 1);
    }

    float getCordura(){
        return cordura;
    }
}
