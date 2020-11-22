using System;
using UnityEngine;

public class ObjectInteraction : Interactive
{
    public String text;

    public override void OnClick() => print(text);
}
