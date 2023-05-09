using UnityEngine;
namespace CarmaRoad.Animal
{
    public class RunState : IBaseState
    {
        public void OnEnterState(AnimalStateManager animal)
        {
            animal.AnimalController.PlayAnimation(Enum.AnimalAnimationClip.Run);

            animal.AnimalController.SetModelBoolValues(false, true, false);
        }

        public void OnFixedUpdate(AnimalStateManager animal)
        {
            animal.AnimalController.MoveAnimal();
        }

        public void OnTriggerEnter2D(AnimalStateManager animal, Collider2D collider2D)
        {
            if (collider2D.gameObject.TryGetComponent<Player.CarView>(out _))
            {
                if (animal.AnimalController.AnimalModel.AnimalType == Enum.AnimalType.Small)
                {
                    // increase speed and animation. for small animals
                    animal.AnimalController.AnimalView.animatorController.speed = 5f;
                    animal.AnimalController.AnimalModel.IsFastRunning = true;
                }
            }
        }

        public void OnTriggerExit2D(AnimalStateManager animal, Collider2D collider2D)
        {
            if (collider2D.gameObject.TryGetComponent<Player.CarView>(out _))
            {
                if (animal.AnimalController.AnimalModel.AnimalType == Enum.AnimalType.Small)
                {
                    // speed and animation speed should be back to normal. for small animals
                    animal.AnimalController.AnimalView.animatorController.speed = 1f;
                    animal.AnimalController.AnimalModel.IsFastRunning = false;
                }

                if (animal.AnimalController.AnimalModel.AnimalType == Enum.AnimalType.Large)
                {
                    // return to walkling state
                    animal.ChangeState(animal.walkState);
                }
            }
        }
    }
}
