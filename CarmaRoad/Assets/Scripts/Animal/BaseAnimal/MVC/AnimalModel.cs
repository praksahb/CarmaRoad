public class AnimalModel
{
    public AnimalModel(BaseAnimalObject animalSO)
    {
        AnimalType = animalSO.animalType;
        walkSpeed = animalSO.walkSpeed;
        runSpeed = animalSO.runSpeed;
        FreezeTime = animalSO.freezeTime;
    }

    // public read-only properties
    public AnimalType AnimalType { get; }
    public float FreezeTime { get; }
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
}
