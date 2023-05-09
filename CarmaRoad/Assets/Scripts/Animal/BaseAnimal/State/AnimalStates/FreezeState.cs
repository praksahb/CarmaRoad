using UnityEngine;
namespace CarmaRoad.Animal
{
    public class FreezeState : IBaseState
    {
        public void OnEnterState(AnimalStateManager animal)
        {
            animal.AnimalController.PlayAnimation(Enum.AnimalAnimationClip.Idle);
            animal.AnimalController.SetModelBoolValues(true, false, false);
        }

        public void OnFixedUpdate(AnimalStateManager animal)
        {

        }

        public void OnTriggerEnter2D(AnimalStateManager animal, Collider2D collider2D)
        {
            // player is already inside trigger collider of animal if in this state.
        }

        public void OnTriggerExit2D(AnimalStateManager animal, Collider2D collider2D)
        {
            animal.ChangeState(animal.walkState);
        }
    }
}
