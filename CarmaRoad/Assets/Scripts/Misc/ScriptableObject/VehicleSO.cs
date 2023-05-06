using UnityEngine;

namespace CarmaRoad.Player
{
    [CreateAssetMenu(fileName = "VehicleScriptableObject", menuName = "ScriptableObject/VehicleSOList")]

    public class VehicleSO : ScriptableObject
    {
        public BaseVehicle[] allVehicles;
    }


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