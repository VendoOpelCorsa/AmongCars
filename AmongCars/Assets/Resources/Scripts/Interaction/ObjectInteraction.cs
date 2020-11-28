using System;
using TMPro;
using UnityEngine;

public class ObjectInteraction : Interactive
{

    private static TMP_Text titleTX;
    private static TMP_Text displayTX;
    private static GameObject dialoguePanel, icon;

    protected AmongCars controls;

    public string text;
    public string title;

    protected bool open;

    protected void Awake() => controls = new AmongCars();
    protected void OnEnable() => controls?.Enable();
    protected void OnDisable() => controls?.Disable();

    public override void OnInteract()
    {
        open = true;

        ShowDialog(true);
        icon.SetActive(false);
        titleTX.text = title;

        displayTX.text = text;
    }

    protected void OnExit()
    {
        open = false;

        ShowDialog(false);
    }

    void Start()
    {
        if (dialoguePanel != null)
            return;

        dialoguePanel = GameObject.Find("DialoguePanel");

        titleTX = GameObject.Find("NPC Name").GetComponent<TMP_Text>();
        displayTX = GameObject.Find("DisplayText").GetComponent<TMP_Text>();
        icon = GameObject.Find("Icon");

        icon.SetActive(true);
        ShowDialog(false);
    }

    void Update()
    {
        base.Update();

        if (open)
            if (controls.Player.Salir.ReadValue<float>() == 1)
                OnExit();
    }

    public static void ShowDialog(bool b) => dialoguePanel.SetActive(b);

    public static void ShowIcon(bool b) => icon.SetActive(b);
}
