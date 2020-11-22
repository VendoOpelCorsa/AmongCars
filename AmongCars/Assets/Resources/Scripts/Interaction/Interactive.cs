using UnityEngine;

public abstract class Interactive : MonoBehaviour
{
    private bool isLooked;

    public abstract void OnInteract();

    protected void Update()
    {
        if (isLooked)
            OnInteract();
    }

    public void setIsLooked(bool looked)
    {
        isLooked = looked;
    }
}
