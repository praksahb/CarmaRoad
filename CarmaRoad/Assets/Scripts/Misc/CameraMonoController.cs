using UnityEngine;
namespace CarmaRoad.Utils
{

    public class CameraMonoController : MonoBehaviour
    {

        public float smoothSpeed = 1f;
        public float smoothTime = 0.1f;
        public Vector3 locationOffset;

        private Transform target;
        private Vector3 desiredPosition = Vector3.zero;
        private Vector3 smoothedPosition = Vector3.zero;
        private Vector3 currentVelocity;

        private void OnEnable()
        {
            PlayerService.Instance.AssignPlayerTransform += AssignTargetTransform;
            PlayerService.Instance.UnassignPlayerTransform += UnassignTargetTransform;
        }

        private void OnDisable()
        {
            PlayerService.Instance.AssignPlayerTransform -= AssignTargetTransform;
            PlayerService.Instance.UnassignPlayerTransform -= UnassignTargetTransform;
        }

        void LateUpdate()
        {
            if (target == null) return;

            //FollowVehicleUsingSmoothDamp();

            FollowVehicleUsingLerp();
        }

        private void AssignTargetTransform(Transform playerTransform)
        {
            target = playerTransform;
        }

        private void UnassignTargetTransform()
        {
            target = null;
        }

        // this can be used later for when implementing a player being chased by ghost 

        private void FollowVehicleUsingSmoothDamp()
        {
            desiredPosition = target.position + locationOffset;
            desiredPosition.z = transform.position.z;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothTime);
        }

        private void FollowVehicleUsingLerp()
        {
            desiredPosition = target.position + locationOffset;
            smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            smoothedPosition.z = transform.position.z;
            transform.position = smoothedPosition;
        }
    }
}