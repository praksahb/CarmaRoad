using System;
using UnityEngine;

public class PlayerService : GenericMonoSingleton<PlayerService>
{
    [SerializeField] private VehicleSO vehicleList;

    private Vector2 spawnPoint;

    public CarController CarController { get; private set; }

    private void Start()
    {
        spawnPoint = Vector2.zero;
    }

    public Action<Transform> AssignPlayerTransform;
    public Action UnassignPlayerTransform;
    public Action<CarController> AssignControllerRef;

    public void CreateVehiclePublic()
    {
        CreateVehicle();

        // assign transform ref to camera controller
        AssignPlayerTransform?.Invoke(CarController.CarView.transform);
        //assign car controller ref to animalSpawner
        AssignControllerRef?.Invoke(CarController);
    }

    private void CreateVehicle()
    {
        BaseVehicle baseVehicle = vehicleList.allVehicles[(int)CarType.Ambulance];

        CarModel carModel = new CarModel(baseVehicle);
        CarController = new CarController(carModel, baseVehicle.vehiclePrefab, spawnPoint);
    }

    public Action HeadLightOnOff;
    public Action<bool> EmergencyLightOnOff;
}
