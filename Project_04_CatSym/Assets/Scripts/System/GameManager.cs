using UnityEngine;

namespace CatSym
{
    public class GameManager : MonoBehaviour
    {
        //STATICS
        public static GameManager _GM;
        public static GameObject player;

        void Awake()
        {
            if (_GM == null)
            {
                _GM = this;
            }

            else
            {
                Destroy(gameObject);
            }

            player = GameObject.FindGameObjectWithTag("Player");
        }

        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}