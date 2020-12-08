using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;

public class Work : MonoBehaviour
{
    public AmongCars controls;
    public GameObject panel;
    public TMP_Text taskText;

    public float time = 1;

    private List<string> tasks;

    protected void Awake() => controls = new AmongCars();
    protected void OnEnable() => controls?.Enable();
    protected void OnDisable() => controls?.Disable();

    void Start()
    {
        tasks = new List<string>();
        tasks.Add("Vete fuera donde los contenedores a sincronizar los generadores");
        tasks.Add("El ordenador del jefe no funciona, ¡¡ARRÉGLALO!!");
        tasks.Add("Vaya... tienes suerte. No hay nada que hacer... de momento");
        tasks.Add("Acuérdate... Siempre puedes llamar a la policía para resolver el caso");
        tasks.Add("El ordenador del jefe no funciona... ARRÉGLALO!!!");
        tasks.Add("Precinta las cajas del almacén, vago!");
        tasks.Add("Es bromi");
        panel.SetActive(false);
        StartCoroutine(TimeToWork());
    }

    IEnumerator TimeToWork()
    {
        yield return new WaitForSeconds(time); 
        panel.SetActive(true);
        GenerateTask();
        yield return new WaitForSeconds(5); 
        reset();
    }

    void GenerateTask()
    {
        int index = Random.Range(0, tasks.Count);
        taskText.text = tasks[index];
    }

    void reset()
    {
        panel.SetActive(false);
        StartCoroutine(TimeToWork());
    }
}
