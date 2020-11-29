using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMovement : MonoBehaviour
{

    public AmongCars controls;

    private CharacterController cntrl;

    void Start()
    {
        cntrl = GetComponent<CharacterController>();

    }
    protected void Awake() => controls = new AmongCars();
    protected void OnEnable() => controls.Enable();
    protected void OnDisable() => controls.Disable();

    public bool on = false;

    void Update()
    {
        bool start = controls.Player.Start.ReadValue<float>() == 1;
        if(start){
            SceneManager.LoadScene("City");
        }
    }

}
