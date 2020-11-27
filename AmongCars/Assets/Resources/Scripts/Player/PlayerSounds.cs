using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    GameObject reloj;

    GameObject motor;

    GameObject gotas;

    int arrancada = 0;
    int risa = 0;
    int goteo = 0;
    int ducha = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider collider){
        if (collider.tag == "Oficina1"){
            reloj = GameObject.FindWithTag("Reloj1");
            AudioSource audio = reloj.GetComponent<AudioSource>();
            if (!audio.isPlaying)
                audio.Play();
        }

        if (collider.tag == "Oficina2"){
            reloj = GameObject.FindWithTag("Reloj2");
            AudioSource audio = reloj.GetComponent<AudioSource>();
            if (!audio.isPlaying)
                audio.Play();
        }

        if (collider.tag == "Oficina3"){
            reloj = GameObject.FindWithTag("Reloj3");
            AudioSource audio = reloj.GetComponent<AudioSource>();
            if (!audio.isPlaying)
                audio.Play();
        }

        if (collider.tag == "Oficina4"){
            reloj = GameObject.FindWithTag("Reloj4");
            AudioSource audio = reloj.GetComponent<AudioSource>();
            if (!audio.isPlaying)
                audio.Play();
        }

        if (collider.tag == "SalidaOficina4"){
            reloj = GameObject.FindWithTag("Reloj4");
            AudioSource audio = reloj.GetComponent<AudioSource>();
            audio.Stop();
        }

        if (collider.tag == "Cafeteria"){
            reloj = GameObject.FindWithTag("Reloj5");
            AudioSource audio = reloj.GetComponent<AudioSource>();
                if (!audio.isPlaying)
                    audio.Play();
        }

        if (collider.tag == "pi"){
            AudioSource audio = collider.GetComponent<AudioSource>();
            if (!audio.isPlaying)
                audio.Play();
        }

        if (collider.tag == "Arranca"){
            arrancada++;
            if (arrancada == 3){
                motor = GameObject.FindWithTag("Motor");
                AudioSource audio = motor.GetComponent<AudioSource>();
                audio.Play();
            }
        }

        if (collider.tag == "Risa"){
            risa++;
            if (risa == 5){
                collider.GetComponent<AudioSource>().Play();
            }
        }

        if (collider.tag == "Vestuario1"){
            gotas = GameObject.FindWithTag("Goteo");
            gotas.GetComponent<AudioSource>().Play();
        }

        if (collider.tag == "Vestuario1"){
            gotas = GameObject.FindWithTag("Goteo");
            if (goteo%2==0)
                gotas.GetComponent<AudioSource>().Play();
            else
                gotas.GetComponent<AudioSource>().Stop();
            goteo++;
        }

        if (collider.tag == "Vestuario2"){
            ducha++;
            gotas = GameObject.FindWithTag("Ducha");
            if (ducha==2)
                gotas.GetComponent<AudioSource>().Play();
        }

        if (collider.tag == "CierraDucha"){
            gotas = GameObject.FindWithTag("Ducha");
            if (gotas.GetComponent<AudioSource>().isPlaying){
                gotas.GetComponent<AudioSource>().Stop();
                gotas = GameObject.FindWithTag("CerrarDucha");
                gotas.GetComponent<AudioSource>().Play();
            }
        }
    }

    public void OnTriggerExit(Collider collider){
        if (collider.tag == "Oficina1"){
            reloj = GameObject.FindWithTag("Reloj1");
            AudioSource audio = reloj.GetComponent<AudioSource>();
            audio.Stop();
        }

        if (collider.tag == "Oficina2"){
            reloj = GameObject.FindWithTag("Reloj2");
            AudioSource audio = reloj.GetComponent<AudioSource>();
            audio.Stop();
        }

        if (collider.tag == "Oficina3"){
            reloj = GameObject.FindWithTag("Reloj3");
            AudioSource audio = reloj.GetComponent<AudioSource>();
            audio.Stop();
        }

        if (collider.tag == "Cafeteria"){
            reloj = GameObject.FindWithTag("Reloj5");
            AudioSource audio = reloj.GetComponent<AudioSource>();
            audio.Stop();
        }
    }
}
