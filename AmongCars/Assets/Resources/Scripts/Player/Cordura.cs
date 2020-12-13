using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cordura : MonoBehaviour
{   
    public float cordura, corduraTotal = 100f;

    public Image textoCordura;

    public GameObject panelFinal;
    public Text panelSimon;
    public Text panelAcusar;

    void Start()
    {
        cordura = corduraTotal;
    }

    void quitarCordura(){
        cordura = Mathf.Clamp(cordura - (corduraTotal/3), 0f, corduraTotal);
        print(cordura);
        textoCordura.transform.localScale = new Vector2( cordura/corduraTotal, 1);

        if(cordura == 0)
        {
           panelSimon.GetComponent<Text>().text = "";
           panelAcusar.GetComponent<Text>().text = "";
           panelFinal.SetActive(true);
           FinJuego();
           SceneManager.LoadScene("MainMenu"); 
        }
    }

    IEnumerator FinJuego()
    {
        print("entro");
        yield return new WaitForSeconds(20); 
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
