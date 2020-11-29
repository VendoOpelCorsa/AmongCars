using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Acusar : MonoBehaviour
{
    protected AmongCars controls;

    private List<string> names;
    private List<string> weapons;

    public GameObject panelAcusar;

    public TMP_Text textArma;
    public TMP_Text textAsesino;

    public GameObject textoGanado;
    public GameObject textoSigueIntentandolo;

    public Button izqArma;
    public Button izqAsesino;

    public Button derArma;
    public Button derAsesino;
    
    public Button botonAcusar;

    public GameObject player;

    private bool pulsado;

    private int arma;
    private int asesino;

    protected void Awake() => controls = new AmongCars();
    protected void OnEnable() => controls?.Enable();
    protected void OnDisable() => controls?.Disable();

    // Start is called before the first frame update
    void Start()
    {
        names = new List<string>();
        weapons = new List<string>();

        names.Add("NARANJA");
        names.Add("TURQUESA");
        names.Add("MORADO");
        names.Add("AÑIL");
        names.Add("ROSITA");

        weapons.Add("DESTORNILLADOR");
        weapons.Add("LLAVE INGLESA");
        weapons.Add("CUCHILLO");
        weapons.Add("TORNILLOS");

        textAsesino.text = names[0];
        textArma.text = weapons[0];

        arma = 0;
        asesino = 0;
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            panelAcusar.SetActive(true);
        }
    }

    void OnTriggerStay(Collider other){

        if(other.CompareTag("Player")){
            StartCoroutine(CambioBotones());
        }

    }

    void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            textoSigueIntentandolo.SetActive(false);
            textoGanado.SetActive(false);
            panelAcusar.SetActive(false);
        }
    }

    IEnumerator CambioBotones(){
        if (!pulsado && controls.Player.Option1.ReadValue<float>() == 1){
            pulsado = true;
            textAsesino.text = names[asesino];
            asesino --;
            if(asesino == -1){
                 asesino = 4;
            }
            yield return new WaitForSeconds(0.1f);
            pulsado = false;
        }
        if (!pulsado && controls.Player.Option2.ReadValue<float>() == 1){
            pulsado = true;
            textAsesino.text = names[asesino];
            asesino ++;
            if(asesino > 4){
                 asesino = 0;
            }
            yield return new WaitForSeconds(0.1f);
            pulsado = false;
        }
        if (!pulsado && controls.Player.Option3.ReadValue<float>() == 1){
            pulsado = true;
            textArma.text = weapons[arma];
            arma --;
            if(arma == -1){
                 arma = 3;
            }
            yield return new WaitForSeconds(0.1f);
            pulsado = false;
            
        }
        if (!pulsado && controls.Player.Option4.ReadValue<float>() == 1){
            pulsado = true;
            textArma.text = weapons[arma];
            arma ++;
            if(arma > 3){
                arma = 0;
            }
            yield return new WaitForSeconds(0.1f);
            pulsado = false;
        }
        if(!pulsado && controls.Player.Acusar.ReadValue<float>() == 1){
            pulsado = true;
            if(textAsesino.text != names[4] || textArma.text != weapons[1]){
                player.SendMessage("quitarCordura");
                textoSigueIntentandolo.SetActive(true);
                panelAcusar.SetActive(false);
            }
            if(textAsesino.text == names[4] && textArma.text == weapons[1]){
                print("correcto!");
                panelAcusar.SetActive(false);
                textoGanado.SetActive(true);
            }
            yield return new WaitForSeconds(0.1f);
            pulsado = false;
        }
    }
}
