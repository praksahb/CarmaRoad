namespace CarmaRoad.Animal
{
    public class AnimalModel
    {
        public AnimalModel(BaseAnimalObject animalSO)
        {
            AnimalType = animalSO.animalType;
            walkSpeed = animalSO.walkSpeed;
            runSpeed = animalSO.runSpeed;
            FreezeTime = animalSO.freezeTime;
            RunSpeedModifier = animalSO.runSpeedModifier;
        }

        // public read-only properties
        public Enum.AnimalType AnimalType { get; }
        public float FreezeTime { get; }  // not being used currently anywhere
        public float RunSpeedModifier { get; }
        // private read-only fields
        private readonly float walkSpeed;
        private readonly float runSpeed;

        // public read-only property
        public float CurrentSpeed
        {
            get
            {
                return IsIdle ? 0f : IsRunning ? runSpeed : walkSpeed;
            }
        }

        // public read-write property
        public bool IsRunning { get; set; }
        public bool IsIdle { get; set; }
        public bool IsFastRunning { get; set; }
    }
}
