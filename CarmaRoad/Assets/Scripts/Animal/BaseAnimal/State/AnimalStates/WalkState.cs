using UnityEngine;

public class WalkState : IBaseState
{
    public void OnEnterState(AnimalStateManager animal)
    {
        animal.AnimalController.PlayAnimation(AnimalAnimationClip.Walk);
    }

    public void OnFixedUpdate(AnimalStateManager animal)
    {
        animal.AnimalController.MoveAnimal();
    }

    public void OnTriggerEnter2D(AnimalStateManager animal, Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<CarView>(out _))
        {
            if (animal.AnimalController.AnimalModel.AnimalType == AnimalType.Large)
            {
                animal.ChangeState(animal.freezeState);
            }

            if (animal.AnimalController.AnimalModel.AnimalType == AnimalType.Human)
            {
                // turn back?
                // needs custom logic to check to see if they can cross road or not
                // or just turn them back
                animal.AnimalController.ChangeDirection();
            }
        }
    }
}
