using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimalSpawnerState
{
    void EnterState(AnimalSpawner animalSpawner);
    void ExitState(AnimalSpawner animalSpawner);
    //void Update(AnimalSpawner animalSpawner);
}
