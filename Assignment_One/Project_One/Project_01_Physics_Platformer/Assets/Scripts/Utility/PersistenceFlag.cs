using UnityEngine;

namespace PhysicsSym
{
    public class PersistenceFlag : MonoBehaviour
    {
        public static PersistenceFlag Instance;
        public bool cleanUp = true;

        void Start()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }

        public void CleanUp()
        {
            if (cleanUp)
            {
                Destroy(gameObject);
            }
        }

    }

}
