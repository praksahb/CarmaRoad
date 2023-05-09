using UnityEngine;
namespace CarmaRoad.Animal
{
    public class WalkState : IBaseState
    {
        public void OnEnterState(AnimalStateManager animal)
        {
            animal.AnimalController.PlayAnimation(Enum.AnimalAnimationClip.Walk);
            animal.AnimalController.SetModelBoolValues(false, false, false);
        }

        public void OnFixedUpdate(AnimalStateManager animal)
        {
            animal.AnimalController.MoveAnimal();
        }

        public void OnTriggerEnter2D(AnimalStateManager animal, Collider2D collider2D)
        {
            if (collider2D.gameObject.TryGetComponent<Player.CarView>(out _))
            {
                PerformTypeSpecificAction(animal);
            }
        }

        public void OnTriggerExit2D(AnimalStateManager animal, Collider2D collider2D)
        {
            if (collider2D.gameObject.TryGetComponent<Player.CarView>(out _))
            {
                if (animal.AnimalController.AnimalModel.AnimalType == Enum.AnimalType.Large)
                {
                    animal.AnimalController.ChangeDirection();
                }
            }
        }

        private void PerformTypeSpecificAction(AnimalStateManager animal)
        {
            if (animal.AnimalController.AnimalModel.AnimalType == Enum.AnimalType.Human)
            {
                animal.ChangeState(animal.freezeState);
            }

            if (animal.AnimalController.AnimalModel.AnimalType == Enum.AnimalType.Large)
            {
                animal.AnimalController.ChangeDirection();
            }
        }
    }
}
