using UnityEngine;

public class DeadState : IBaseState
{
    public void OnEnterState(AnimalStateManager animal)
    {
        animal.AnimalController.PlayAnimation(AnimalAnimationClip.Dead);
        animal.AnimalController.StopMoving();

        // destroy animal game object. can disable it and turn it off 
        // if using object pool
        Object.Destroy(animal.AnimalController.AnimalView.gameObject, 5f);

    }

    public void OnFixedUpdate(AnimalStateManager animal)
    {
    }

    public void OnTriggerEnter2D(AnimalStateManager animal, Collider2D collision)
    {
        // do nothing to dead animal
    }
}
