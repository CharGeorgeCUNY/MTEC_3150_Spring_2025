using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace WinterSym
{
    public class PlayerScanner : MonoBehaviour
    {
        [Header("Ray Data")]
        [SerializeField] private float RayLength = 0.9f;

        [Header("Virtual Objects")]
        [SerializeField] private GameObject virtualFloor;

        [Header("Layer Masks")]
        [SerializeField] private LayerMask maskedLayers;

        public HitData ScanFloor()
        {
            // Debug.Log("PlayerScanner: " + "Scanning!");
            var hitData = new HitData();

            var bottomOrigin = virtualFloor.transform.position;

            hitData.HitFound = Physics.Raycast(virtualFloor.transform.position, -transform.up, out hitData.Hit, RayLength);

            Debug.DrawRay(virtualFloor.transform.position, -transform.up * RayLength, hitData.HitFound ? Color.red : Color.white);
            // Debug.Log(hitData.HitFound); 

            if (hitData.HitFound)
            {
                hitData.HitObject = hitData.Hit.collider.gameObject;
                hitData.HitPoint = hitData.Hit.point;

                // Debug.Log("PlayerScanner: " + "Hit a " + hitData.HitObject + " at " + hitData.HitPoint);
            }

            return hitData;
        }

        public GroundFlagType GetGroundFlagType()
        {
            HitData hitData = ScanFloor();
            if (hitData.HitFound && hitData.HitObject.GetComponent<UtilityFlags>() != null)
                return hitData.HitObject.GetComponent<UtilityFlags>().GroundFlag;

            else
                return GroundFlagType.none;
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
