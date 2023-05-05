using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace KarmaRoad.Assets.Scripts.Services.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TileManager tilesSpawn;
        [SerializeField] private AnimalSpawner animalSpawner;

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

        private void StartGame()
        {
            // create player
            PlayerService.Instance.CreateVehiclePublic();
            // create tiles - infy scrolling
            tilesSpawn.GenerateTilePrefab();

            // start animal Spawner
            animalSpawner.ChangeState(animalSpawner.waitingState);
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