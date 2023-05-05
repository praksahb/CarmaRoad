using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IBaseState
{
    public void OnEnterState(AnimalStateManager animal)
    {
        animal.AnimalController.PlayAnimation(AnimalAnimationClip.Run);
    }

    public void OnFixedUpdate(AnimalStateManager animal)
    {
        bool animalIsRunning = true;
        animal.AnimalController.MoveAnimal(animalIsRunning);
    }

    public void OnTriggerEnter2D(AnimalStateManager animal, Collider2D collision)
    {
        // doing nothing for now or increase speed more by adding speed modifier to animal model
    }
}