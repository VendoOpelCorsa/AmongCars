using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Work : MonoBehaviour
{
    public AmongCars controls;
    public GameObject panel;
    public float time = 1;

    protected void Awake() => controls = new AmongCars();
    protected void OnEnable() => controls?.Enable();
    protected void OnDisable() => controls?.Disable();

    void Start()
    {
        panel.SetActive(false);
        StartCoroutine(TimeToWork());
    }

    IEnumerator TimeToWork()
    {
        yield return new WaitForSeconds(time); 
        panel.SetActive(true);
        yield return new WaitForSeconds(5); 
        reset();
    }

    void reset()
    {
        panel.SetActive(false);
        StartCoroutine(TimeToWork());
    }
}
