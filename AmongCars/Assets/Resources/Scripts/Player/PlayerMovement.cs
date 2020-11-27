using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public AmongCars controls;

    public float jumpSpeed = 5.3f;
    public float sprintMultiplier = 1.3f;
    public float speed = 4.5f;

    private float g = 9.81f;
    
    private Light flashlight;
    private Light fComponent;

    private CharacterController cntrl;

    private float directionY;

    private float moved;

    void Start()
    {
        cntrl = GetComponent<CharacterController>();
        flashlight = GetComponentInChildren<Light>();
        fComponent = flashlight.GetComponent<Light>();
    }
    protected void Awake() => controls = new AmongCars();
    protected void OnEnable() => controls.Enable();
    protected void OnDisable() => controls.Disable();

    public bool on = false;

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

        bool flashLight = controls.Player.ToggleFlashLight.ReadValue<float>() == 1;

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
        
        if (flashLight){
             if(on){
                fComponent.enabled = true;
                on = !on;
             }
            else if(!on){
                fComponent.enabled = false;
                on = !on;
            }
        }

        if (!grounded)
            directionY -= g * 1.4f * Time.deltaTime;

        else if (directionY < 0)
            directionY = -g / 2;

        dir.y = directionY;

        cntrl.Move(dir * Time.deltaTime);

        moved += new Vector2(dir.x,dir.z).magnitude * Time.deltaTime;

        if (moved > 2.5f) {
            moved = 0;
            PlaySound();
        }
    }

    bool IsOnGround() => cntrl.isGrounded;

    private void PlaySound() {
        GameObject paso = GameObject.FindWithTag("Paso");
        AudioSource audio = paso.GetComponent<AudioSource>();
        if (!audio.isPlaying) {
            audio.Play();
        }
    }
}
