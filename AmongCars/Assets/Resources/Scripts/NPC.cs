using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NPC file", menuName = "NPC files Archive")]
public class NPC : ScriptableObject
{
    public string name;
    public string icon;

    [TextArea(3, 15)]
    public string[] sentences1;

    [TextArea(3, 15)]
    public string[] sentences2;

    [TextArea(3, 15)]
    public string[] playerResponses;

    private int round = 1;

    public string[] GetSentences()
    {
        return round == 1 ? sentences1 : sentences2;
    }

    public string GetSentence(int index)
    {
        return GetSentences()[index];
    }

    public string[] GetOptions()
    {
        return playerResponses.Skip((round - 1) * 3).Take(3).ToArray();
    }

    public string GetResponse(int index)
    {
        return playerResponses[index];
    }

    public int GetRound() => round;

    public void ChangeRound()
    {
        round = round == 1 ? 2 : 1;
    }
}
