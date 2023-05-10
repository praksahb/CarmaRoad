using UnityEngine;
namespace CarmaRoad.Animal
{
    [RequireComponent(typeof(IBaseState))]
    public class AnimalStateManager : MonoBehaviour
    {
        public AnimalController AnimalController { get; set; }
        public IBaseState CurrentState { get; private set; }

        public FreezeState freezeState = new FreezeState();
        public WalkState walkState = new WalkState();
        public RunState runState = new RunState();
        public DeadState deadState = new DeadState();

        private float velocityModifier;

        private void Start()
        {
            // AnimalController reference does not get assigned when awake is called so currently calling in start.
            // assigning ref in ctor of animal controller
            SpawnState();
        }

        // currently using only two initial state values. human can have a different state. where they check the road before crossing depending on vehicle distance.
        private void SpawnState()
        {
            if (AnimalController.AnimalModel.AnimalType == Enum.AnimalType.Small)
            {
                CurrentState = runState;
            }
            //else if (AnimalController.AnimalModel.AnimalType == AnimalType.Large || AnimalType.Human
            else
            {
                CurrentState = walkState;
            }
            CurrentState.OnEnterState(this);
        }

        public void ChangeState(IBaseState state)
        {
            CurrentState = state;
            state.OnEnterState(this);
        }

        private void FixedUpdate()
        {
            CurrentState.OnFixedUpdate(this);
        }

        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            CurrentState.OnTriggerEnter2D(this, collider2D);
        }

        private void OnTriggerExit2D(Collider2D collider2D)
        {
            CurrentState.OnTriggerExit2D(this, collider2D);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (CurrentState is DeadState) return;
#pragma warning disable IDE0001
            if (collision.gameObject.TryGetComponent<Player.CarView>(out Player.CarView vehicle))
#pragma warning restore IDE0001
            {
                // re adjust vehicle/ car velocity
                float vehicleSpeed = vehicle.PrevVelocityMagnitude;
                float speedModifier = vehicleSpeed / vehicle.CarController.CarModel.MaxSpeed;
                vehicle.Rb2d.velocity = vehicleSpeed * speedModifier * velocityModifier * vehicle.transform.up;

                // stop rb on animal
                AnimalController.AnimalView.RBody2D.simulated = false;

                // vehicle has hit the animal
                KarmaaManager.Instance.ReduceKarma();

                // send animal to death state
                ChangeState(deadState);
            }
        }
    }
}
