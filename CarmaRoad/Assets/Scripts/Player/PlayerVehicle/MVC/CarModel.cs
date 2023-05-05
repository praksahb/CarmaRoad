public class CarModel 
{
    public CarModel(BaseVehicle baseVehicle)
    {
        CarType = baseVehicle.carType;
        SpeedRate = baseVehicle.accelerationFactor;
        TurnRate = baseVehicle.turnFactor;
        DriftRate = baseVehicle.driftFactor;
        MaxSpeed = baseVehicle.maxSpeed;
    }

    public CarType CarType { get; }
    public float SpeedRate { get; }
    public float TurnRate { get; }
    public float DriftRate { get; }
    public float MaxSpeed { get; }
}
