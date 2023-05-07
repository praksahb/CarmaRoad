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
    }
}
