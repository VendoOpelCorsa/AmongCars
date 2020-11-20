using UnityEngine;

public abstract class ButtonColliderClick : ColliderClick
{
    private Material material;

    private Color baseColor;
    private readonly Color hoverColor = new Color(0, 200, 200);

    void Start()
    {
        material = GetComponent<Renderer>().material;
        baseColor = material.color;
    }

    public abstract override void OnClick();

    public override void OnEnter()
    {
        material.color = hoverColor;
    }

    public override void OnExit()
    {
        material.color = baseColor;
    }
}
