using UnityEngine;

public class CarController
{
    public CarController(CarModel carModel, CarView carView, Vector3 spawnPoint)
    {
        CarModel = carModel;
        CarView = Object.Instantiate(carView, spawnPoint, Quaternion.identity);
        CarView.CarController = this;

        CarMoveInput = new VehicleMovementController();
    }

    public CarModel CarModel { get; }
    public CarView CarView { get; }
    public VehicleMovementController CarMoveInput { get; }


    // movement logic 
    public void MoveForward(float accelerationInput)
    {
        // Get forward direction in terms of velocity
        float forwardVelocity = Vector2.Dot(CarView.transform.up, CarView.Rb2d.velocity);
        float maxSpeed = CarModel.MaxSpeed;

        // limit going above forwardVelocity
        if (forwardVelocity > maxSpeed && accelerationInput > 0)
        {
            return;
        }

        // in reverse limit going above 1/2  * forwardVelocity
        if (forwardVelocity < -maxSpeed * 0.5f && accelerationInput < 0)
        {
            return;
        }

        // limit in all directions
        if (CarView.Rb2d.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
        {
            return;
        }

        // apply drag - on moving vehicle
        if (forwardVelocity > 0)
        {
            // apply higher drag on negative acceleration = braking action
            if(accelerationInput < 0)
            {
                CarView.Rb2d.drag = Mathf.Lerp(CarView.Rb2d.drag, 3f, Time.fixedDeltaTime * 3);
            }
            // apply little drag if no acceleration input
            else if(accelerationInput == 0)
            {
                CarView.Rb2d.drag = Mathf.Lerp(CarView.Rb2d.drag, 0.1f, Time.fixedDeltaTime);
            }
            else
            {
                CarView.Rb2d.drag = 0;
            }
        }
        else
        {
            CarView.Rb2d.drag = 0;
        }

        // create accelerating force
        Vector2 forceVector = accelerationInput * CarModel.SpeedRate * CarView.transform.up;

        // add force
        CarView.Rb2d.AddForce(forceVector, ForceMode2D.Force);
    }

    public void RotateSteering(float steeringInput)
    {
        // limit rotation ability when at lower speed
        float minSpeedBeforeAllowTurning = CarView.Rb2d.velocity.magnitude / 8;

        minSpeedBeforeAllowTurning = Mathf.Clamp01(minSpeedBeforeAllowTurning);

        // update rotation based on input
        float rotationAngle = CarView.Rb2d.rotation;
        rotationAngle -= steeringInput * CarModel.TurnRate * minSpeedBeforeAllowTurning;

        CarView.Rb2d.MoveRotation(rotationAngle);
    }

    public void ReduceSideVelocity()
    {
        Vector2 forwardSpeed = CarView.transform.up * Vector2.Dot(CarView.Rb2d.velocity, CarView.transform.up);
        Vector2 sideVelocity = CarView.transform.right * Vector2.Dot(CarView.Rb2d.velocity, CarView.transform.right);

        CarView.Rb2d.velocity = forwardSpeed + sideVelocity * CarModel.DriftRate;
    }

}
