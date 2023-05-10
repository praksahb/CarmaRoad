using System.Collections.Generic;
using UnityEngine;
namespace CarmaRoad.Animal
{
    public class AnimalController
    {
        // read-only properties
        public AnimalModel AnimalModel { get; }
        public AnimalView AnimalView { get; }
        //  might have to provide ASM with write access if changing states from animalView or here 
        public AnimalStateManager AnimalStateManager { get; }

        // dictionary for storing hash values of animationClips string names
        private Dictionary<Enum.AnimalAnimationClip, int> animationClips;
        public float velocityModifier;

        // PUBLIC LEVEL -
        // Ctor - handles individual animal - used in CreateAnimalService
        public AnimalController(AnimalModel animalModel, AnimalView animalView, Vector2 spawnPoint, Enum.AnimalSpawnPosition animalSpawnPos)
        {
            AnimalModel = animalModel;

            Quaternion rotation = SetFacingDirection(animalSpawnPos);
            AnimalView = Object.Instantiate(animalView, spawnPoint, rotation);
            AnimalView.AnimalController = this;


            AnimalStateManager = AnimalView.GetComponent<AnimalStateManager>();
            AnimalStateManager.AnimalController = this;

            InitializeAnimationClipDictionary();
            SetVelocityModifier();
        }

        public void SetModelBoolValues(bool isIdle, bool isRunning, bool isFastRunning)
        {
            AnimalModel.IsIdle = isIdle;
            AnimalModel.IsRunning = isRunning;
            AnimalModel.IsFastRunning = isFastRunning;
        }

        public void OnVehicleCollision(Player.CarView vehicle)
        {
            // re adjust vehicle/ car velocity
            float vehicleSpeed = vehicle.PrevVelocityMagnitude;
            float speedModifier = vehicleSpeed / vehicle.CarController.CarModel.MaxSpeed;
            vehicle.Rb2d.velocity = vehicleSpeed * speedModifier * velocityModifier * vehicle.transform.up;

            // stop rb on animal
            AnimalView.RBody2D.simulated = false;

            // vehicle has hit the animal
            KarmaaManager.Instance.ReduceKarma();

            // send animal to death state
            AnimalStateManager.ChangeState(AnimalStateManager.deadState);
        }

        // move function for animal
        public void MoveAnimal()
        {
            // create initial velocity from speed and direction
            Vector2 velocity = new Vector2(AnimalModel.CurrentSpeed, 0) * AnimalView.transform.right;
            // add more if needed
            velocity = AnimalModel.IsFastRunning ? velocity * AnimalModel.RunSpeedModifier : velocity;
            // get distance and increment it using MovePosition function
            Vector2 deltaMove = AnimalView.RBody2D.position + velocity * Time.fixedDeltaTime;
            AnimalView.RBody2D.MovePosition(deltaMove);
        }
        // stops moving animal
        public void StopMoving()
        {
            AnimalView.RBody2D.velocity = Vector2.zero;
        }
        //change direction
        public void ChangeDirection()
        {
            Quaternion newRotation = AnimalView.transform.rotation * Quaternion.Euler(0, 180, 0);
            AnimalView.transform.rotation = Quaternion.Slerp(AnimalView.transform.rotation, newRotation, 1f);
        }

        // plays animation according to  state
        public void PlayAnimation(Enum.AnimalAnimationClip clip)
        {
            int hashVal;

            if (animationClips.TryGetValue(clip, out hashVal))
            {
                AnimalView.animatorController.Play(hashVal);
            }
            else
            {
                Debug.LogError($"Key name: {clip} doesnt exist");
            }
        }

        // Private Level -

        // changes transform.right value of animal to point in left or right direction of the road
        private Quaternion SetFacingDirection(Enum.AnimalSpawnPosition animalSpawnPos)
        {
            Quaternion facingDirection = animalSpawnPos == Enum.AnimalSpawnPosition.Left ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
            return facingDirection;
        }
        // initialize hash values for animation clip names
        private void InitializeAnimationClipDictionary()
        {
            animationClips = new Dictionary<Enum.AnimalAnimationClip, int>();

            for (int i = (int)Enum.AnimalAnimationClip.Idle; i <= (int)Enum.AnimalAnimationClip.Dead; i++)
            {
                string clipName = GetAnimClipName((Enum.AnimalAnimationClip)i);
                int hashValue = Animator.StringToHash(clipName);
                animationClips.Add((Enum.AnimalAnimationClip)i, hashValue);
            }
        }
        // helper for above func
        private string GetAnimClipName(Enum.AnimalAnimationClip clip)
        {
            string clipName = "";
            switch (clip)
            {
                case Enum.AnimalAnimationClip.Idle:
                    clipName = "Idle";
                    break;
                case Enum.AnimalAnimationClip.Walk:
                    clipName = "Walk";
                    break;
                case Enum.AnimalAnimationClip.Run:
                    clipName = "Run";
                    break;
                case Enum.AnimalAnimationClip.Dead:
                    clipName = "Dead";
                    break;
                default:
                    Debug.LogError("Invalid animation clip");
                    break;
            }
            return clipName;
        }

        // Velocity used in collision between vehicle and animal
        private void SetVelocityModifier()
        {
            switch (AnimalModel.AnimalType)
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
    }
}
