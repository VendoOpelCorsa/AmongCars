using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Interactive : MonoBehaviour
{

    public virtual void WasClicked()
    {
        /*string str = "Click " + gameObject.name;
        Debug.Log(str);*/
        blocked = true;

        if (isLooked)
            OnExit();

        OnClick();

        blocked = false;
    }

    public abstract void OnClick();

    private bool blocked = false;
    private bool isLooked = false;
    public float timerDuration = 1.25f;
    private float lookTimer = 0f;

    protected void Update()
    {
        if (isLooked)
        {
            lookTimer += Time.deltaTime;
            if (lookTimer > timerDuration)
            {
                lookTimer = -5f;
                //Debug.Log("Object timer click");
                WasClicked();
            }
        }
        else
        {
            lookTimer = 0f;
        }
    }

    public void setIsLooked(bool looked)
    {
        if (blocked)
            return;

        if (looked)
            OnEnter();
        else
            OnExit();

        isLooked = looked;
    }

    public virtual void OnEnter() { }
    public virtual void OnExit() { }

    void OnDisable() => OnExit();
}
