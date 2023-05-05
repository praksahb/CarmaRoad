using UnityEngine;


public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] private TileManager tileScroller;
    [SerializeField] private AnimalSO animalList;
    [SerializeField] private float lowerSpawnTime = 0.5f;
    [SerializeField] private float upperSpawnTime = 5f;
    [SerializeField] private float lowerSpawnDist = 5f;
    [SerializeField] private float upperSpawnDist = 5f;

    public float LowerTime
    {
        get { return lowerSpawnTime; }
    }

    public float UpperTime
    {
        get { return upperSpawnTime; }
    }

    private float rightSpawn;
    private float leftSpawn;

    private AnimalSpawnPosition currentSpawningDirection;
    private CarController playerCarController;

    private void Awake()
    {
        currentSpawningDirection = AnimalSpawnPosition.Left;
    }

    private void OnEnable()
    {
        tileScroller.SendPositionValue += GetPosition;
        PlayerService.Instance.AssignControllerRef += SetPlayerCarController;
    }

    private void OnDisable()
    {
        tileScroller.SendPositionValue -= GetPosition;
        PlayerService.Instance.AssignControllerRef -= SetPlayerCarController;
    }

    private void SetPlayerCarController(CarController carController)
    {
        playerCarController = carController;
    }

    private void GetPosition(float val, AnimalSpawnPosition leftOrRight)
    {
        switch (leftOrRight)
        {
            case AnimalSpawnPosition.Right:
                rightSpawn = val;
                break;
            case AnimalSpawnPosition.Left:
                leftSpawn = val;
                break;
            default:
                // debug log or error
                break;
        }
    }

    public void SpawnAnimal()
    {
        // random animal to be spawned
        AllAnimals animalType = GetRandomAnimal();
        BaseAnimalObject baseAnimal = GetBaseAnimal(animalType);

        // Get Spawn Position close to vehicle
        SpawnPosition(baseAnimal);

        // create the animal.
        CreateAnimalService.Instance.CreateAnimal(baseAnimal, spawnPos, currentSpawningDirection);
    }

    private void SpawnPosition(BaseAnimalObject baseAnimal)
    {
        // // Get Spawn Position X
        if (currentSpawningDirection == AnimalSpawnPosition.Left)
        {
            currentSpawningDirection = AnimalSpawnPosition.Right;
            spawnPos.x = rightSpawn;
        }
        else
        {
            currentSpawningDirection = AnimalSpawnPosition.Left;
            spawnPos.x = leftSpawn;
        }

        // // Get Spawn Position Y
        OffsetForSpawnYPosition(baseAnimal);
    }

    private void OffsetForSpawnYPosition(BaseAnimalObject baseAnimal)
    {
        // main formula
        // spawnPos.y = yPos + yVeloSign * (distanceOffsetX * playerVeloY / animalVeloX) + offsetY

        // origin/reference point
        float yPos = playerCarController.CarView.transform.position.y;
        // distance to be covered by animal in x-axis
        float offsetX = Mathf.Abs(spawnPos.x - playerCarController.CarView.transform.position.x);
        // speed of animal
        float animalSpeed = baseAnimal.animalType == AnimalType.Small ? baseAnimal.runSpeed : baseAnimal.walkSpeed;
        // time 
        float timeToReachXPos = Mathf.Abs(offsetX) / animalSpeed;
        // Calculate idealYposition for animal
        float finalYPos = yPos + timeToReachXPos * playerCarController.CarView.Rb2d.velocity.y;
        // add offset to it
        float offsetY = Random.Range(lowerSpawnDist, upperSpawnDist);

        // final y position
        spawnPos.y = finalYPos + offsetY;
    }

    private AllAnimals GetRandomAnimal()
    {
        return (AllAnimals)Random.Range(0, animalList.allAnimals.Length);
    }

    private BaseAnimalObject GetBaseAnimal(AllAnimals animalIndex)
    {
        return animalList.allAnimals[(int)animalIndex];
    }

    // state related - Spawning and Waiting
    public IAnimalSpawnerState CurrentState { get; private set; }

    public SpawningState spawningState = new SpawningState();
    public WaitingState waitingState = new WaitingState();
    private Vector2 spawnPos;

    public void ChangeState(IAnimalSpawnerState state)
    {
        if (CurrentState != null)
        {
            CurrentState.ExitState(this);
        }
        if(state == null)
        {
            return;
        }

        CurrentState = state;
        state.EnterState(this);
    }
}