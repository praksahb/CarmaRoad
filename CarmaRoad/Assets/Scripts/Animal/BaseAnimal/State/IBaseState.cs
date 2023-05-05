using UnityEngine;

public interface IBaseState 
{
    public void OnEnterState(AnimalStateManager animal);
    public void OnFixedUpdate(AnimalStateManager animal);
    void OnTriggerEnter2D(AnimalStateManager animal, Collider2D collider);
}
