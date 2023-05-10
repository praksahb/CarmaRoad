using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace CarmaRoad
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Utils.TileManager tilesSpawn;
        [SerializeField] private Animal.Spawner.AnimalSpawner animalSpawner;

        [SerializeField] private Light2D globalLight;

        private void Start()
        {
            UIManager.Instance.StartGameCall += StartGame;
            UIManager.Instance.GameOverCall += GameIsOver;
        }

        private void OnDisable()
        {
            UIManager.Instance.StartGameCall -= StartGame;
            UIManager.Instance.GameOverCall -= GameIsOver;
        }

        private void SelectVehicle()
        {

        }

        private void StartGame()
        {
            // create player
            PlayerService.Instance.CreateVehiclePublic();
            // create tiles - infy scrolling
            tilesSpawn.GenerateTilePrefab();

            // start animal Spawner
            animalSpawner.ChangeState(animalSpawner.waitingState);
<<<<<<< Updated upstream
=======
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
>>>>>>> Stashed changes
        }


        private void GameIsOver()
        {
            animalSpawner.ChangeState(null);

            //disable move controller
            PlayerService.Instance.CarController.CarMoveInput.DisableController();

            // remove transform ref from camera controller
            PlayerService.Instance.UnassignPlayerTransform?.Invoke();

            // global light on
            globalLight.color = Color.white;
        }
    }
}