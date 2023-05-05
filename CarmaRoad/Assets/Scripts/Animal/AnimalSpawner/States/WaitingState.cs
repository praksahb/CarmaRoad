using System.Collections;
using UnityEngine;

public class WaitingState : IAnimalSpawnerState
{
    private Coroutine waitForSpawnCoroutine;
    private WaitForSecondsRealtime waitTimeSpawning;

    public void EnterState(AnimalSpawner animalSpawner)
    {
        // if using a coroutine i can directly call coroutine here?
        // then wont even need to use a update function in my IAnimalSpawnerState interface
        waitForSpawnCoroutine = animalSpawner.StartCoroutine(WaitForRandomTime(animalSpawner));
    }

    private IEnumerator WaitForRandomTime(AnimalSpawner animalSpawner)
    {
        float randomTimeValue = UnityEngine.Random.Range(animalSpawner.LowerTime, animalSpawner.UpperTime);
        waitTimeSpawning = new WaitForSecondsRealtime(randomTimeValue);
        yield return waitTimeSpawning;
        animalSpawner.ChangeState(animalSpawner.spawningState);
    }

    public void ExitState(AnimalSpawner animalSpawner)
    {
        if (waitForSpawnCoroutine != null)
        {
            animalSpawner.StopCoroutine(waitForSpawnCoroutine);
            waitForSpawnCoroutine = null;
        }
        //Debug.Log("Exiting State: " + animalSpawner.CurrentState);
    }
}
