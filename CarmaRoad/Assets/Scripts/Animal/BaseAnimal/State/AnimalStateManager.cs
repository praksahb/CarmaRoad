using UnityEngine;
namespace CarmaRoad.Animal
{
    public class AnimalStateManager : MonoBehaviour
    {
        public IBaseState CurrentState { get; private set; }

        public FreezeState freezeState = new FreezeState();
        public WalkState walkState = new WalkState();
        public RunState runState = new RunState();
        public DeadState deadState = new DeadState();
        private float velocityModifier;

        public AnimalController AnimalController { get; set; }

        private void Start()
        {
            SpawnState();
            SetVelocityModifier();
        }

        private void SetVelocityModifier()
        {
            switch (AnimalController.AnimalModel.AnimalType)
            {
                case Enum.AnimalType.Small:
                    velocityModifier = 0.75f;
                    break;
                case Enum.AnimalType.Large:
                    velocityModifier = 0.5f;
                    break;
                case Enum.AnimalType.Human:
                    velocityModifier = 0.5f;
                    break;
                default:
                    velocityModifier = 0.5f;
                    break;
            }
        }

        // currently using only two initial state values. human can have a different state. where they check the road before crossing depending on vehicle distance.
        private void SpawnState()
        {
            if (AnimalController.AnimalModel.AnimalType == Enum.AnimalType.Small)
            {
                CurrentState = runState;
            }
            //else if (AnimalController.AnimalModel.AnimalType == AnimalType.Large)
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            CurrentState.OnTriggerEnter2D(this, collision);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (CurrentState is DeadState) return;
            if (collision.gameObject.TryGetComponent<Player.CarView>(out Player.CarView vehicle))
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
