using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public AmongCars controls;

    public float jumpSpeed = 5.3f;
    public float sprintMultiplier = 1.3f;
    public float speed = 4.5f;

    private float g = 9.81f;

    private CharacterController cntrl;

    private float directionY;

    void Start()
    {
        cntrl = GetComponent<CharacterController>();
    }

    protected void Awake() => controls = new AmongCars();
    protected void OnEnable() => controls.Enable();
    protected void OnDisable() => controls.Disable();

    void Update()
    {
        if (transform.position.y < -20)
        {
            transform.position = new Vector3(transform.position.x, 2, transform.position.z);
            return;
        }

        Vector2 input = controls.Player.Move.ReadValue<Vector2>();
        float x = input.x;
        float z = input.y;

        bool sprint = controls.Player.Sprint.ReadValue<float>() == 1;
        bool jump = controls.Player.Jump.ReadValue<float>() == 1;

        var c = VREmulator.GetRotation();
        Vector3 dir = c * Vector3.right * x + c * Vector3.forward * z;

        bool grounded = IsOnGround();

        if (!grounded)
            dir *= .7f;

        float actualSpeed = speed;
        if (sprint)
            actualSpeed *= sprintMultiplier;

        dir *= actualSpeed;

        if (jump && grounded)
            directionY = jumpSpeed;

        if (!grounded)
            directionY -= g * 1.4f * Time.deltaTime;

        else if (directionY < 0)
            directionY = -g / 2;

        dir.y = directionY;

        cntrl.Move(dir * Time.deltaTime);
    }

    bool IsOnGround() => cntrl.isGrounded;
}
