using UnityEngine;

namespace CarmaRoad.Player
{
    [CreateAssetMenu(fileName = "VehicleScriptableObject", menuName = "ScriptableObject/VehicleSOList")]
    public class VehicleSO : ScriptableObject
    {
        public BaseVehicle[] allVehicles;
    }
}