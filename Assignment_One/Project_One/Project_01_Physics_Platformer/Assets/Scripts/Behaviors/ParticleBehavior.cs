using System.Collections;
using UnityEngine;

namespace PhysicsSym
{
    public class ParticleBehavior : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine("DelayedSelfDestruct");
        }

        private IEnumerator DelayedSelfDestruct()
        {
            yield return new WaitForSecondsRealtime(GameManager.deathTime);
            Destroy(this.gameObject);
        }
    }

}