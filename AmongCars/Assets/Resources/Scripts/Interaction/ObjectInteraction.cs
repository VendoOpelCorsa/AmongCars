using System;
using UnityEngine;

public class ObjectInteraction : Interactive
{
    public String text;

    public override void OnClick(){
        print(text);

    /*
        player = other.gameObject;
        dialoguePanel.SetActive(true);
        npcName.text = npc.name;
        string url = "Sprites/" + npc.icon;
        npcImage.sprite = Resources.Load<Sprite>(url);
        StartDialogue();   
        */ 
    } 


}
