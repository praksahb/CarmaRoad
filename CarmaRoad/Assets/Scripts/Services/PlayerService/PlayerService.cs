using CarmaRoad.Player;
using System;
using UnityEngine;

namespace CarmaRoad
{
    public class PlayerService : GenericMonoSingleton<PlayerService>
    {
        [SerializeField] private VehicleSO vehicleList;

        public CarController CarController { get; private set; }

        private Vector2 spawnPoint;
        private int index = 0;
        private int vehicleListLength;

        private void Start()
        {
            spawnPoint = Vector2.zero;
            vehicleListLength = vehicleList.allVehicles.Length;
        }

        public Action<Transform> AssignPlayerTransform;
        public Action UnassignPlayerTransform;
        public Action<CarController> AssignControllerRef;

        public void CreateVehiclePublic(int plusOrMinusOne)
        {
            index = (index + plusOrMinusOne + vehicleListLength) % vehicleListLength;

            CreateVehicle(index);

            // assign transform ref to camera controller
            AssignPlayerTransform?.Invoke(CarController.CarView.transform);
            //assign car controller ref to animalSpawner
            AssignControllerRef?.Invoke(CarController);
        }

        public void DestroyVehicle()
        {
            Destroy(CarController.CarView.gameObject);
        }

        private void CreateVehicle(int index)
        {
            BaseVehicle baseVehicle = vehicleList.allVehicles[index];

            CarModel carModel = new CarModel(baseVehicle);
            CarController = new CarController(carModel, baseVehicle.vehiclePrefab, spawnPoint);
        }

        public Action<bool> HeadLightOnOff;
        public Action<bool> EmergencyLightOnOff;
    }
}
