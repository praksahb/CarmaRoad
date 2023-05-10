namespace CarmaRoad.Player
{
    [System.Serializable]
    public class BaseVehicle
    {
        public CarView vehiclePrefab;
        public Enum.CarType carType;
        public float accelerationFactor;
        public float turnFactor;
        public float driftFactor;
        public float maxSpeed;
    }
}
