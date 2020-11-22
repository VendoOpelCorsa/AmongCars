using UnityEngine;

public abstract class Interactive : MonoBehaviour
{
    private bool isLooked;

    public abstract void OnInteract();

    protected virtual void OnExit() { }

    protected void Update()
    {
        if (isLooked)
            OnInteract();
        /*else
            OnExit();*/
    }

    public void setIsLooked(bool looked)
    {
        isLooked = looked;
    }
}
