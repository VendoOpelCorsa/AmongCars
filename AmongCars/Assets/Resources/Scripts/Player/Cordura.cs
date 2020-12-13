using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cordura : MonoBehaviour
{   
    protected AmongCars controls;

    public float cordura, corduraTotal = 100f;

    public Image textoCordura;

    public GameObject panelFinal;
    public Text panelSimon;
    public Text panelAcusar;

    public GameObject panelInicio;

    protected void Awake() => controls = new AmongCars();
    protected void OnEnable() => controls?.Enable();
    protected void OnDisable() => controls?.Disable();

    void Start()
    {
        cordura = corduraTotal;
    }

    void Update()
    {
        if(controls.Player.Salir.ReadValue<float>() == 1)
        {
            panelInicio.SetActive(false);
        }
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
