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
            UIManager.Instance.StartGameCall += StartGame;
            UIManager.Instance.GameOverCall += GameIsOver;
            UIManager.Instance.RestartGameCall += RestartGame;
        }

        private void OnDisable()
        {
            UIManager.Instance.StartGameCall -= StartGame;
            UIManager.Instance.GameOverCall -= GameIsOver;
            UIManager.Instance.RestartGameCall -= RestartGame;
        }

        private void SelectVehicle()
        {

        }

        private void StartGame()
        {
            // create player
            playerServiceInstance.CreateVehiclePublic();
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