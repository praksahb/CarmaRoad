using UnityEngine;
namespace CarmaRoad.Animal
{
    public interface IBaseState
    {
        void OnEnterState(AnimalStateManager animal);
        void OnFixedUpdate(AnimalStateManager animal);
        void OnTriggerEnter2D(AnimalStateManager animal, Collider2D collider);
        void OnTriggerExit2D(AnimalStateManager animal, Collider2D collider);
    }
}
