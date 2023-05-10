namespace CarmaRoad.Animal
{
    [System.Serializable]
    public class BaseAnimal
    {
        public AnimalView animalPrefab;
        public Enum.AllAnimals AnimalName;
        public Enum.AnimalType animalType;
        public float walkSpeed;
        public float runSpeed;
        public float freezeTime;
        public float runSpeedModifier;
    }
}
