using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CarView : MonoBehaviour
{
    public Rigidbody2D Rb2d { get; private set; }
    public CarController CarController { get; set; }

    public float PrevVelocityMagnitude { get; private set; }

    private Animator carAnimController;
    private int eLightOn;
    private int eLightOff;

    private float accelerationInput = 0;
    private float steeringInput = 0;

    [SerializeField] private Light2D headlightLeft;
    [SerializeField] private Light2D headlightRight;

    private void Awake()
    {
        Rb2d = GetComponent<Rigidbody2D>();
        carAnimController = GetComponent<Animator>();
        eLightOn = Animator.StringToHash("LightOn");
        eLightOff = Animator.StringToHash("LightOff");
    }

    private void OnEnable()
    {
        PlayerService.Instance.HeadLightOnOff += SwitchHeadLights;
        PlayerService.Instance.EmergencyLightOnOff += SwitchEmergencyLights;
    }

    private void OnDisable()
    {
        PlayerService.Instance.HeadLightOnOff -= SwitchHeadLights;
        PlayerService.Instance.EmergencyLightOnOff -= SwitchEmergencyLights;

    }

    private void FixedUpdate()
    {
        SetInputVector(CarController.CarMoveInput.MoveDirInput);
        CarController.MoveForward(accelerationInput);
        CarController.RotateSteering(steeringInput);
        CarController.ReduceSideVelocity();

        PrevVelocityMagnitude = Rb2d.velocity.magnitude;

    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    private void SwitchHeadLights()
    {
            headlightLeft.enabled = !headlightLeft.enabled;
            headlightRight.enabled = !headlightRight.enabled;
    }

    private void SwitchEmergencyLights(bool isLightOn)
    {
        if(isLightOn)
        {
            carAnimController.Play(eLightOn);
        } 
        else
        {
            carAnimController.Play(eLightOff);
            carAnimController.Play(eLightOff);
        }
    }
}
