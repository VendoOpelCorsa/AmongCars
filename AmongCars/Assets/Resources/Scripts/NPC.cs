using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NPC file", menuName = "NPC files Archive")]
public class NPC : ScriptableObject
{
    public string name;
    public string icon;

    [TextArea(3, 15)]
    public string[] sentences;
    
     [TextArea(3, 15)]
    public string[] playerResponses;

    public AudioClip[] voces;

    public string[] GetSentences()
    {   
        return sentences;
    }

    public string GetSentence(int index)
    {
        return sentences[index];
    }

    public AudioClip GetAudio(int index) 
    {
        return voces[index];
    }

    public string[] GetOptions()
    {
        return playerResponses;
    }

    public string GetResponse(int index)
    {
        return playerResponses[index];
    }

}
