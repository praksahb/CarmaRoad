using UnityEngine;
using UnityEngine.InputSystem;
namespace CarmaRoad.Player
{
    public class VehicleMovementController : PlayerInputActions.IVehicleActions
    {
        public Vector2 MoveDirInput { get; private set; }

        private bool headlightSwitch;
        public bool HeadlightSwitch
        {
            get
            {
                return headlightSwitch;
            }
            set
            {
                headlightSwitch = value;
                PlayerService.Instance.HeadLightOnOff?.Invoke(headlightSwitch);
            }
        }

        private bool emergencyLightSwitch;
        public bool EmergencyLightSwitch
        {
            get { return emergencyLightSwitch; }
            set
            {
                emergencyLightSwitch = value;
                PlayerService.Instance.EmergencyLightOnOff?.Invoke(emergencyLightSwitch);
            }
        }


        private PlayerInputActions playerControls;

        public VehicleMovementController()
        {
            playerControls = new PlayerInputActions();
            playerControls.Vehicle.SetCallbacks(this);
            playerControls.Vehicle.Enable();
            headlightSwitch = true;
            emergencyLightSwitch = true;
        }

        ~VehicleMovementController()
        {
            playerControls.Vehicle.Disable();
        }

        public void DisableController()
        {
            playerControls.Vehicle.Disable();
        }

        public void EnableController()
        {
            playerControls.Vehicle.Enable();

        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveDirInput = context.ReadValue<Vector2>();
        }

        public void OnHeadLight(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                HeadlightSwitch = !HeadlightSwitch;
            }
        }

        public void OnEmergencyLight(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                EmergencyLightSwitch = !EmergencyLightSwitch;
            }
        }
    }
}