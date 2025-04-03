using Unity.Entities;
using UnityEngine;

namespace JumpSym.Player
{
    public class WorldScanner : MonoBehaviour
    {
        [SerializeField] private float frontRayOffset = 0.25f;
        [SerializeField] private float frontRayLengthH = 2.5f;
        [SerializeField] private float frontRayLengthV = 5f;

        [SerializeField] private float chestRayOffset = 0f;
        [SerializeField] private float chestRayLength = 2f;
        
        [SerializeField] private float bottomRayLengthV = 0.2f;

        [SerializeField] private float cameraRayLengthH = 1f;

        [SerializeField] private Transform footBone;

        [SerializeField] private LayerMask obstacleLayer;
        [SerializeField] private LayerMask floorLayer;
        [SerializeField] private LayerMask playerLayer;

        public HitData ScanFloor()
        {
            var hitData = new HitData();

            var bottomOrigin = transform.position;

            // ~ means ignore
            hitData.HitFoundV = Physics.Raycast(bottomOrigin, -transform.up,
                 out hitData.HitV, bottomRayLengthV, ~playerLayer);

            // Debug.DrawRay(bottomOrigin, -transform.up * bottomRayLengthV, hitData.HitFoundV ? Color.red : Color.white);

            if (hitData.HitFoundV)
            {
                hitData.HitObject = hitData.HitV.collider.gameObject;
                hitData.HitPoint = hitData.HitV.point;
            }

            return hitData;
        }

        public HitData ScanCamera()
        {
            var hitData = new HitData();

            var frontTransform = Controller.playerCamera.transform;

            //shoot a ray from the camera towards what it is looking at
            hitData.HitFoundH = Physics.Raycast(frontTransform.position, frontTransform.forward, out hitData.HitH, 
                cameraRayLengthH, obstacleLayer);

            Debug.DrawRay(frontTransform.position, transform.forward * cameraRayLengthH, hitData.HitFoundH ? Color.red : Color.white);

            if (hitData.HitFoundH)
            {
                hitData.HitObject = hitData.HitH.collider.gameObject;
                hitData.HitPoint = hitData.HitH.point;
            }

            return hitData;
        }

        public HitData ScanWall()
        {
            var hitData = new HitData();

            var frontOrigin = transform.position + new Vector3(0, frontRayOffset, 0);

            //shoot a ray at knee height in the forward direction, output raycast hit to our hitData variable + return if i hit anything, only for the obstacle layer
            hitData.HitFoundH = Physics.Raycast(frontOrigin, transform.forward, out hitData.HitH, frontRayLengthH, obstacleLayer);

            // Debug.DrawRay(frontOrigin, transform.forward * frontRayLengthH, hitData.HitFoundH ? Color.red : Color.white);


            if (hitData.HitFoundH)
            {
                //grab the gameobject you hit
                hitData.HitObject = hitData.HitH.collider.gameObject;

                //create a new point above the horizontal raycast hit at frontRayLengthV distance
                var frontOriginHeight = hitData.HitH.point + Vector3.up * frontRayLengthV;

                //shoot a ray up from the ground to this new point, output raycast hit to hitData + return if I hit anything
                hitData.HitFoundV = Physics.Raycast(frontOriginHeight, Vector3.down, out hitData.HitV, frontRayLengthV, obstacleLayer);

                // Debug.DrawRay(frontOriginHeight, Vector3.down * frontRayLengthV, hitData.HitFoundV ? Color.red : Color.white);
            }
            
            return hitData;

        }

        public HitData ScanHang()
        {
            var hitData = new HitData();

            var chestOrigin = transform.position + new Vector3(0, chestRayOffset, 0);

            hitData.HitFoundH = Physics.Raycast(chestOrigin, transform.forward, out hitData.HitH, chestRayLength, obstacleLayer);
            Debug.DrawRay(chestOrigin, transform.forward * chestRayLength, hitData.HitFoundH ? Color.red : Color.white);

            return hitData;
        }

        public struct HitData
        {
            //store the gameobject hit
            public GameObject HitObject;

            //store the hit location
            public Vector3 HitPoint;

            //store if you hit anything
            public bool HitFoundH;
            public bool HitFoundV;

            //store hit locations and data
            public RaycastHit HitH;
            public RaycastHit HitV;
        }
    }
}
