using CarmaRoad.Animal;
using UnityEngine;

namespace CarmaRoad
{
    public class CreateAnimalService : GenericMonoSingleton<CreateAnimalService>
    {
        private AnimalController animalController;

        public AnimalController CreateAnimal(BaseAnimalObject baseAnimal, Vector2 spawnPoint, Enum.AnimalSpawnPosition animalSpawnPosition)
        {
            AnimalModel animalModel = new AnimalModel(baseAnimal);
            animalController = new AnimalController(animalModel, baseAnimal.animalPrefab, spawnPoint, animalSpawnPosition);
            return animalController;
        }
    }
}
