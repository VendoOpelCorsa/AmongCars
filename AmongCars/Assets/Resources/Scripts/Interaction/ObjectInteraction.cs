using System;
using TMPro;
using UnityEngine;

public class ObjectInteraction : Interactive
{

    private AmongCars controls;

    public String text;
    public String title;

    private TMP_Text titleTX;
    private TMP_Text displayTX;
    private GameObject dialoguePanel;

    private bool open;

    protected void Awake() => controls = new AmongCars();
    protected void OnEnable() => controls?.Enable();
    protected void OnDisable() => controls?.Disable();

    public override void OnClick()
    {
        open = true;
        print(text);

        dialoguePanel.SetActive(true);
        titleTX.text = title;

        displayTX.text = text;
    }

    void Start()
    {
        dialoguePanel = GameObject.FindWithTag("dialoguePanel");

        titleTX = GameObject.FindWithTag("title").GetComponent<TMP_Text>();
        displayTX = GameObject.FindWithTag("displayText").GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (open)
            if (controls.Player.Salir.ReadValue<float>() == 1)
            {
                dialoguePanel.SetActive(false);
                open = false;
            }
    }
}
