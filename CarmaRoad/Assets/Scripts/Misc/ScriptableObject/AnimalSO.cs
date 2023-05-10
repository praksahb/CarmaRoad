using UnityEngine;

namespace CarmaRoad.Animal
{
    [CreateAssetMenu(fileName = "AnimalScriptableObject", menuName = "ScriptableObject/AnimalSOList")]
    public class AnimalSO : ScriptableObject
    {
        public BaseAnimal[] allAnimals;
    }
}