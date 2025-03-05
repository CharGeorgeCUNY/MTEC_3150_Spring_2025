using System.Collections;
using UnityEngine;

namespace PhysicsSym
{
    public class PlayerEffects : MonoBehaviour
    {
        private TrailRenderer trailRenderer;
        public GameObject deathSparklesPrefab;
        public static ContactPoint2D myContact;
        public static PlayerEffects currentEffects;
        
        void Awake()
        {
            currentEffects = this;
        }

        void Start()
        {
            trailRenderer = GetComponent<TrailRenderer>();
            trailRenderer.widthMultiplier = 2;
        }

        public void DeathSparkles()
        {
            Quaternion rotation = PlayerController.rb2D.transform.rotation;
            Vector3 position = myContact.point;

            GameObject deathSparkles = Instantiate(deathSparklesPrefab, position, rotation);

        }

    }

}
