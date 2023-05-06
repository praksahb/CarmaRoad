namespace CarmaRoad.Animal.Spawner
{
    public class SpawningState : IAnimalSpawnerState
    {
        public void EnterState(AnimalSpawner animalSpawner)
        {
            // Spawn The animal here.
            animalSpawner.SpawnAnimal();

            // transition back to waiting state.
            animalSpawner.ChangeState(animalSpawner.waitingState);
        }

        public void ExitState(AnimalSpawner animalSpawner)
        {
            //Debug.Log("exiting state: " + animalSpawner.CurrentState);
        }

    }
}
