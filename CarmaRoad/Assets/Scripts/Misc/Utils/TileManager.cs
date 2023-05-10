using System;
using System.Collections.Generic;
using UnityEngine;

// for the purpose of infinite scrolling, 
// should try with using object pooling.

namespace CarmaRoad.Utils
{

    public class TileManager : MonoBehaviour
    {
        public GridLayout tilePrefab;

        private GridLayout tile1;
        private GridLayout tile2;
        private BoxCollider2D bc;

        private List<BoxCollider2D> RoadCollidersList;

        private float tileLength;

        private Transform cameraTransform;

        private Vector3 tile1TargetPosition = new Vector3();
        private Vector3 tile2TargetPosition = new Vector3();

        private Vector3 offsetPosition;

        private void Awake()
        {
            cameraTransform = Camera.main.transform;
            bc = tilePrefab.GetComponent<BoxCollider2D>();
            tileLength = bc.size.y;
        }

        public void GenerateTilePrefab()
        {
            // Spawn 2 tiles
            tile1 = UnityEngine.Object.Instantiate(tilePrefab);
            offsetPosition = tile1.transform.position;
            offsetPosition.y += tileLength;
            tile2 = UnityEngine.Object.Instantiate(tilePrefab, offsetPosition, Quaternion.identity);

            // get colliders for use in animal spawner
            RoadCollidersList = new List<BoxCollider2D>();
            tile2.GetComponents(RoadCollidersList);

            GetPositionX();
        }

        void FixedUpdate()
        {
            if (tile1 == null || tile2 == null) return;
            if (cameraTransform.position.y >= tile2.transform.position.y)
            {
                SetPosition(out tile1TargetPosition, tile1.transform.position.x, tile2.transform.position.y + tileLength, tile1.transform.position.z);
                tile1.transform.position = tile1TargetPosition;
                SwitchTiles();
            }

            if (cameraTransform.position.y <= tile1.transform.position.y)
            {
                SetPosition(out tile2TargetPosition, tile2.transform.position.x, tile1.transform.position.y - tileLength, tile2.transform.position.z);
                tile2.transform.position = tile2TargetPosition;
                SwitchTiles();
            }
        }

        private void SwitchTiles()
        {
            (tile2, tile1) = (tile1, tile2);
        }

        private void SetPosition(out Vector3 position, float x, float y, float z)
        {
            position.x = x;
            position.y = y;
            position.z = z;
        }

        public Action<float, Enum.AnimalSpawnPosition> SendPositionValue;

        private void GetPositionX()
        {
            for (int i = 0; i < RoadCollidersList.Count; i++)
            {
                if (RoadCollidersList[i].bounds.center.x < tile2.transform.position.x)
                {
                    SendPositionValue?.Invoke(RoadCollidersList[i].bounds.center.x, Enum.AnimalSpawnPosition.Left);
                }

                if (RoadCollidersList[i].bounds.center.x > tile2.transform.position.x)
                {
                    SendPositionValue?.Invoke(RoadCollidersList[i].bounds.center.x, Enum.AnimalSpawnPosition.Right);
                }

            }
        }
    }
}
