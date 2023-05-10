using UnityEngine;

namespace CarmaRoad.Animal.Spawner
{
    [RequireComponent(typeof(IAnimalSpawnerState))]
    public class AnimalSpawner : MonoBehaviour
    {
        [SerializeField] private Utils.TileManager tileScroller;
        [SerializeField] private AnimalSO animalList;
        [SerializeField] private float lowerSpawnTime = 3f;
        [SerializeField] private float upperSpawnTime = 6f;
        [SerializeField] private float lowerSpawnDist = 2f;
        [SerializeField] private float upperSpawnDist = 5f;

        // read-only properties
        public float LowerTime
        {
            get { return lowerSpawnTime; }
        }

        public float UpperTime
        {
            get { return upperSpawnTime; }
        }

        public IAnimalSpawnerState CurrentState { get; private set; }

        // public state variables
        public SpawningState spawningState = new SpawningState();
        public WaitingState waitingState = new WaitingState();
        private Vector2 spawnPos;

        private float rightSpawn;
        private float leftSpawn;

        private bool gameOverFlag;
        private float defaultYPos;

        private Enum.AnimalSpawnPosition currentSpawningDirection;
        private Player.CarController playerCarController;

        private void Awake()
        {
            currentSpawningDirection = Enum.AnimalSpawnPosition.Left;
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


        public void ChangeState(IAnimalSpawnerState state)
        {
            if (CurrentState != null)
            {
                CurrentState.ExitState(this);
            }
            if (state == null)
            {
                return;
            }

            CurrentState = state;
            state.EnterState(this);
        }
        // called in spawningState
        public void SpawnAnimal()
        {
            // random animal to be spawned
            Enum.AllAnimals animalType = GetRandomAnimal();
            BaseAnimal baseAnimal = GetBaseAnimal(animalType);

            // Get Spawn Position close to vehicle
            SpawnPosition(baseAnimal);

            // create the animal.
            CreateAnimalService.Instance.CreateAnimal(baseAnimal, spawnPos, currentSpawningDirection);
        }

        public void SwitchGameOverFlag(bool IsGameOver)
        {
            if (IsGameOver)
            {
                gameOverFlag = true;
                defaultYPos = Camera.main.transform.position.y;
            }
            else
            {
                gameOverFlag = false;
            }
        }

        // Event Handler - initialize starting positions for spawnposition.x
        private void GetPosition(float val, Enum.AnimalSpawnPosition leftOrRight)
        {
            switch (leftOrRight)
            {
                case Enum.AnimalSpawnPosition.Right:
                    rightSpawn = val;
                    break;
                case Enum.AnimalSpawnPosition.Left:
                    leftSpawn = val;
                    break;
                default:
                    // debug log or error
                    break;
            }
        }

        private void SetPlayerCarController(Player.CarController carController)
        {
            playerCarController = carController;
        }

        private Enum.AllAnimals GetRandomAnimal()
        {
            return (Enum.AllAnimals)Random.Range(0, animalList.allAnimals.Length);
        }

        private BaseAnimal GetBaseAnimal(Enum.AllAnimals animalIndex)
        {
            return animalList.allAnimals[(int)animalIndex];
        }

        private void SpawnPosition(BaseAnimal baseAnimal)
        {
            // // Get Spawn Position X, then swap it to alternate between the two sides
            if (currentSpawningDirection == Enum.AnimalSpawnPosition.Left)
            {
                currentSpawningDirection = Enum.AnimalSpawnPosition.Right;
                spawnPos.x = rightSpawn;
            }
            else
            {
                currentSpawningDirection = Enum.AnimalSpawnPosition.Left;
                spawnPos.x = leftSpawn;
            }

            // // Get Spawn Position Y
            if (!gameOverFlag)
            {
                OffsetForSpawnYPosition(baseAnimal);
            }
            else
            {
                SimpleOffsetForYPos();
            }

        }

        private void OffsetForSpawnYPosition(BaseAnimal baseAnimal)
        {
            // main formula
            // dont need sign as getting sign value from playerVeloY
            // spawnPos.y = yPos + (playerVeloY * distanceOffsetX / animalVeloX) + offsetY

            // origin/reference point
            float yPos = playerCarController.CarView.transform.position.y;
            // distance to be covered by animal in x-axis
            float offsetX = Mathf.Abs(spawnPos.x - playerCarController.CarView.transform.position.x);
            // speed of animal
            float animalSpeed = baseAnimal.animalType == Enum.AnimalType.Small ? baseAnimal.runSpeed : baseAnimal.walkSpeed;
            // time to cover distance
            float timeToReachXPos = Mathf.Abs(offsetX) / animalSpeed;
            // Calculate idealYposition for animal
            float finalYPos = yPos + timeToReachXPos * playerCarController.CarView.Rb2d.velocity.y;
            // add offset to it
            float offsetY = Random.Range(lowerSpawnDist, upperSpawnDist);

            // final y position
            spawnPos.y = finalYPos + offsetY;
        }

        private void SimpleOffsetForYPos()
        {
            spawnPos.y = defaultYPos + Random.Range(-upperSpawnDist, upperSpawnDist);
        }
    }
}