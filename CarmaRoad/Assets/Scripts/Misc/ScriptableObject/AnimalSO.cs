using UnityEngine;


[CreateAssetMenu(fileName = "AnimalScriptableObject", menuName = "ScriptableObject/AnimalSOList")]

public class AnimalSO : ScriptableObject
{
    public BaseAnimalObject[] allAnimals;
}


// Base Class definition
[System.Serializable]
public class BaseAnimalObject
{
    public AnimalView animalPrefab;
    public string AnimalName;
    public AnimalType animalType;
    public float walkSpeed;
    public float runSpeed;
    public float freezeTime;
} 