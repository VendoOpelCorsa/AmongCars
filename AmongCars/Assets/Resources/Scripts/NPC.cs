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
    public string[] sentences1;

     [TextArea(3, 15)]
    public string[] sentences2;
    
     [TextArea(3, 15)]
    public string[] playerResponses;

    int round = 1;

    public string[] GetSentences()
    {   
        if(round == 1) {
              return sentences1;
        }
        else {
               return sentences2;
        }
      
    }

    public string GetSentence(int index)
    {
        if(round == 1) {
            return sentences1[index];
        }
        else {
             return sentences2[index];
        }
        
    }

    public string[] GetOptions()
    {
        return playerResponses;
    }

    public string GetResponse(int index)
    {
        return playerResponses[index];
    }

     public int GetRound()
    {
        return round;
    }

    public void SetRound(int n)
    {
        round = n;
    }

    public void ChangeRound()
    {
        if(round == 1) {
            round++;
        }
        else {
            round--;
        }
    }

}
