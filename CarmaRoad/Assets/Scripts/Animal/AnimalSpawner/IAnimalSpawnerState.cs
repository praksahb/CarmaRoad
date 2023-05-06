namespace CarmaRoad.Animal.Spawner
{
    public interface IAnimalSpawnerState
    {
        void EnterState(AnimalSpawner animalSpawner);
        void ExitState(AnimalSpawner animalSpawner);
        //void Update(AnimalSpawner animalSpawner);
    }
}
