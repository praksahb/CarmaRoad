using UnityEngine;
namespace CarmaRoad.Animal
{
    public class WalkState : IBaseState
    {
        private Enum.AnimalType animalType;

        public void OnEnterState(AnimalStateManager animal)
        {
            animal.AnimalController.PlayAnimation(Enum.AnimalAnimationClip.Walk);
            animal.AnimalController.SetModelBoolValues(false, false, false);

            animalType = animal.AnimalController.AnimalModel.AnimalType;
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
                if (animalType == Enum.AnimalType.Large)
                {
                    animal.AnimalController.ChangeDirection();
                }
            }
        }

        private void PerformTypeSpecificAction(AnimalStateManager animal)
        {
            if (animalType == Enum.AnimalType.Human)
            {
                animal.ChangeState(animal.freezeState);
            }

            if (animalType == Enum.AnimalType.Large)
            {
                animal.AnimalController.ChangeDirection(); // or freeze and stand still
            }

            if (animalType == Enum.AnimalType.Medium)
            {
                animal.ChangeState(animal.runState);
            }
        }
    }
}
