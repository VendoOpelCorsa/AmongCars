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

    void OnTriggerEnter(){
        panelAcusar.SetActive(true);
        //MostrarAsesino();
    }

    void OnTriggerStay(){
        if (controls.Player.Option1.ReadValue<float>() == 1){
            if(asesino<0) {
                asesino = 4;
                textAsesino.text = names[asesino];
            }
            else {
                textAsesino.text = names[asesino--];
            }
        }
        if (controls.Player.Option2.ReadValue<float>() == 1){
            if(asesino>4) {
                asesino = 0;
                textAsesino.text = names[asesino];
            }else{
                textAsesino.text = names[asesino++];
            }
        }
        if (controls.Player.Option3.ReadValue<float>() == 1){
            if(arma<0){
                arma = 3;
                textArma.text = names[arma];
            } else {
                textArma.text = names[arma--];
            }
            
        }
        if (controls.Player.Option4.ReadValue<float>() == 1){
            if(arma>3) {
                arma = 0;
                textArma.text = names[arma];
            } else {
                textArma.text = names[arma++];
            }
        }


        // if(textAsesino.text != names[4] || textArma.text != weapons[1]){
        //     player.SendMessage("quitarCordura");
        //     textoSigueIntentandolo.SetActive(true);
        //     panelAcusar.SetActive(false);
        // }
        // if(textAsesino.text == names[4] && textArma.text == weapons[1]){
        //     print("correcto!");
        //     panelAcusar.SetActive(false);
        //     textoGanado.SetActive(true);
        // }

    }

    void OnTriggerExit(){
        textoSigueIntentandolo.SetActive(false);
        textoGanado.SetActive(false);
        panelAcusar.SetActive(false);
    }
}
