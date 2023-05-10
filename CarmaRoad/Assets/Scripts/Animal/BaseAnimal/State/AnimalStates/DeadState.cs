using UnityEngine;
namespace CarmaRoad.Animal
{
    public class DeadState : IBaseState
    {
        public void OnEnterState(AnimalStateManager animal)
        {
            animal.AnimalController.PlayAnimation(Enum.AnimalAnimationClip.Dead);
            animal.AnimalController.SetModelBoolValues(true, false, false);
            animal.AnimalController.StopMoving();

            // destroy animal game object. can disable it and turn it off 
            // if using object pool
            Object.Destroy(animal.AnimalController.AnimalView.gameObject, 5f);
        }

        public void OnFixedUpdate(AnimalStateManager animal)
        {
        }

        public void OnTriggerEnter2D(AnimalStateManager animal, Collider2D collider2D)
        {
            // do nothing
        }

        public void OnTriggerExit2D(AnimalStateManager animal, Collider2D collider2D)
        {
            // doing nothing for now
        }
    }
}
