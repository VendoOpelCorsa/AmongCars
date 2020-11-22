using System;
using TMPro;
using UnityEngine;

public class KeyInteraction : ObjectInteraction
{
    public bool Found { private set; get; }

    public override void OnInteract()
    {
        base.OnInteract();

        Found = true;
    }
}
