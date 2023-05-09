using System.Collections;
using UnityEngine;

namespace CarmaRoad.Animal.Spawner
{
    public class WaitingState : IAnimalSpawnerState
    {
        private Coroutine waitForSpawnCoroutine;
        private WaitForSecondsRealtime waitTimeSpawning;

        public void EnterState(AnimalSpawner animalSpawner)
        {
            waitForSpawnCoroutine = animalSpawner.StartCoroutine(WaitForRandomTime(animalSpawner));
        }

        public void ExitState(AnimalSpawner animalSpawner)
        {
            if (waitForSpawnCoroutine != null)
            {
                animalSpawner.StopCoroutine(waitForSpawnCoroutine);
                waitForSpawnCoroutine = null;
            }
        }

        private IEnumerator WaitForRandomTime(AnimalSpawner animalSpawner)
        {
            float randomTimeValue = UnityEngine.Random.Range(animalSpawner.LowerTime, animalSpawner.UpperTime);
            waitTimeSpawning = new WaitForSecondsRealtime(randomTimeValue);
            yield return waitTimeSpawning;
            animalSpawner.ChangeState(animalSpawner.spawningState);
        }
    }
}
