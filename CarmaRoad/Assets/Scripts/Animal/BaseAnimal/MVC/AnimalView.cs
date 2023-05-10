using UnityEngine;

namespace CarmaRoad.Animal
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]

    public class AnimalView : MonoBehaviour
    {
        public Rigidbody2D RBody2D { get; private set; }

        public Animator animatorController { get; private set; }

        public AnimalController AnimalController { get; set; }

        private void Awake()
        {
            RBody2D = GetComponent<Rigidbody2D>();
            animatorController = GetComponent<Animator>();
        }

        private void Start()
        {
            Destroy(this.gameObject, 5f);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (AnimalController.AnimalStateManager.CurrentState is DeadState) return;
#pragma warning disable IDE0001
            if (collision.gameObject.TryGetComponent<Player.CarView>(out Player.CarView vehicle))
#pragma warning restore IDE0001
            {
                AnimalController.OnVehicleCollision(vehicle);
            }
        }
    }
}
