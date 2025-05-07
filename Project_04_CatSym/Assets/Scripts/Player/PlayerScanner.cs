using UnityEngine;

namespace CatSym
{
    public class PlayerScanner : MonoBehaviour
    {
        [Header("Ray Data")]
        [SerializeField] private float RayLength = 0.9f;

        [Header("Layer Masks")]
        [SerializeField] private LayerMask layer;

        [Header("Components")]
        [SerializeField] private Transform virtualFloor;

        public HitData ScanFloor()
        {
            // Debug.Log("PlayerScanner: " + "Scanning!");
            var hitData = new HitData();

            hitData.HitFound = Physics.Raycast(virtualFloor.position, -transform.up, out hitData.Hit, RayLength, layer);

            Debug.DrawRay(virtualFloor.position, -transform.up * RayLength, hitData.HitFound ? Color.red : Color.white);
            // Debug.Log("PlayerScanner: " + hitData.HitFound); 

            if (hitData.HitFound)
            {
                hitData.HitObject = hitData.Hit.collider.gameObject;
                hitData.HitPoint = hitData.Hit.point;

                // Debug.Log("PlayerScanner: " + "Hit a " + hitData.HitObject + " at " + hitData.HitPoint);
            }

            return hitData;
        }

        public struct HitData
        {
            //store the gameobject hit
            public GameObject HitObject;

            //store the hit location
            public Vector3 HitPoint;
            public bool HitFound;

            //store hit locations and data
            public RaycastHit Hit;
        }
    }
}