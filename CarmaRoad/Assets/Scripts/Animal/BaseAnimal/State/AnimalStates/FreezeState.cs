using UnityEngine;
namespace CarmaRoad.Animal
{
    public class FreezeState : IBaseState
    {
        private float freezeTimer;

        public void OnEnterState(AnimalStateManager animal)
        {
            animal.AnimalController.PlayAnimation(Enum.AnimalAnimationClip.Idle);
            freezeTimer = 2f;
        }

        public void OnFixedUpdate(AnimalStateManager animal)
        {
            if (freezeTimer <= 0f)
            {
                freezeTimer = 0f;
                animal.ChangeState(animal.walkState);
            }
            else
            {
                freezeTimer -= Time.fixedDeltaTime;
            }
        }

        public void OnTriggerEnter2D(AnimalStateManager animal, Collider2D collision)
        {

        }
    }
}
