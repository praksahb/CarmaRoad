using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace CarmaRoad
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Utils.TileManager tilesSpawn;
        [SerializeField] private Animal.Spawner.AnimalSpawner animalSpawner;

        [SerializeField] private Light2D globalLight;
        private Color nightLightColor;
        private PlayerService playerServiceInstance;

        private void Awake()
        {
            playerServiceInstance = PlayerService.Instance;
            nightLightColor = globalLight.color;
        }

        private void Start()
        {
            UIManager.Instance.OnDifficultySelectedStartGame += StartGame;
            UIManager.Instance.GameOverCall += GameIsOver;
            UIManager.Instance.RestartGameCall += RestartGame;
            UIManager.Instance.SelectVehicleLeftRight += SelectVehicle;
            UIManager.Instance.SelectDifficultyLevel += SelectDifficulty;
            playerServiceInstance.CreateVehiclePublic(0);
        }

        private void OnDisable()
        {
            UIManager.Instance.OnDifficultySelectedStartGame -= StartGame;
            UIManager.Instance.GameOverCall -= GameIsOver;
            UIManager.Instance.RestartGameCall -= RestartGame;
            UIManager.Instance.SelectVehicleLeftRight -= SelectVehicle;
            UIManager.Instance.SelectDifficultyLevel -= SelectDifficulty;
        }

        private void SelectVehicle(int indexChange)
        {
            // create player
            playerServiceInstance.DestroyVehicle();
            playerServiceInstance.CreateVehiclePublic(indexChange);
        }

        private void SelectDifficulty(Enum.LevelDifficulty levelDifficulty)
        {
            float minSpawnTime = 0, maxSpawnTime = 0, minSpawnDist = 0, maxSpawnDist = 0;

            switch (levelDifficulty)
            {
                case Enum.LevelDifficulty.Easy:
                    {
                        minSpawnTime = 3.5f;
                        maxSpawnTime = 6.5f;
                        minSpawnDist = 5f;
                        maxSpawnDist = 10f;
                        break;
                    }
                case Enum.LevelDifficulty.Medium:
                    {
                        minSpawnTime = 2.5f;
                        maxSpawnTime = 5f;
                        minSpawnDist = 2f;
                        maxSpawnDist = 5f;
                        break;
                    }
                case Enum.LevelDifficulty.Hard:
                    {
                        minSpawnTime = 1.5f;
                        maxSpawnTime = 4f;
                        minSpawnDist = 1f;
                        maxSpawnDist = 3f;
                        break;
                    }
                case Enum.LevelDifficulty.Impossible:
                    {
                        minSpawnTime = 0f;
                        maxSpawnTime = 0.5f;
                        minSpawnDist = 0f;
                        maxSpawnDist = 1.5f;
                        break;
                    }
            }
            Debug.Log("minSpwnTime: " + minSpawnTime + "\nmaxSpawnTime: " + maxSpawnTime + "\nMin Spawn dist: " + minSpawnDist + "\nmaxSpawnDist: " + maxSpawnDist);
            animalSpawner.SetSpawnValues(minSpawnTime, maxSpawnTime, minSpawnDist, maxSpawnDist);

            UIManager.Instance.OnDifficultySelectedStartGame?.Invoke();
        }

        private void StartGame()
        {
            // create tiles - infy scrolling
            tilesSpawn.GenerateTilePrefab();

            // start spawning by assigning value  to animalSpawner.CurrentState
            animalSpawner.ChangeState(animalSpawner.waitingState);
            // switch flag in animal Spawner - for spawning around player
            animalSpawner.SwitchGameOverFlag(false);
        }
        private void RestartGame()
        {
            // re-enable controller
            playerServiceInstance.CarController.CarMoveInput.EnableController();
            // re- assign camera controller
            playerServiceInstance.AssignPlayerTransform?.Invoke(playerServiceInstance.CarController.CarView.transform);
            // lights off again
            globalLight.color = nightLightColor;
            // switch game over flag in animal spawner
            animalSpawner.SwitchGameOverFlag(false);
        }

        private void GameIsOver()
        {
            animalSpawner.SwitchGameOverFlag(true);

            //disable move controller
            playerServiceInstance.CarController.CarMoveInput.DisableController();

            // remove transform ref from camera controller
            playerServiceInstance.UnassignPlayerTransform?.Invoke();

            // global light on
            globalLight.color = Color.red;
        }
    }
}