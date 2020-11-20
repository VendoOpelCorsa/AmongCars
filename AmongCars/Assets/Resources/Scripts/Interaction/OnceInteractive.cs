using UnityEngine;

public abstract class OnceInteractive : Interactive
{

    public override void WasClicked()
    {
        base.WasClicked();

        enabled = false;
    }
}