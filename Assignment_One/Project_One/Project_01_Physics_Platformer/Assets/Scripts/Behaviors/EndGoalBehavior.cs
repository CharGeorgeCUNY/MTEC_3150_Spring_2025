using UnityEngine;
using UnityEngine.UI;

namespace PhysicsSym
{
    public class EndGoalBehavior : MonoBehaviour
    {
        private bool reachedGoal = false;

        [SerializeField]
        private GameObject winMenu;

        [SerializeField]
        private Camera myCamera;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Rigidbody2D>() == PlayerController.rb2D && reachedGoal == false)
            {
                CameraScript.SwitchCameras();
                winMenu.GetComponent<Canvas>().enabled = true;
                reachedGoal = true;
                myCamera.GetComponent<CameraScript>().hold = true;
            }
        }
    }
}
