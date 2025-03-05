using UnityEngine;
using UnityEngine.UIElements;

namespace PhysicsSym
{
    public class ElevatorBehavior : MonoBehaviour
    {
        //fixed variables

        //where the platform moves from and to
        private Vector3 StartPos, EndPos;

        //is the platform returning to its starting position?
        private bool returning = false;

        //editor variables

        //distance the platform moves from its starting position
        //vertical by default
        [SerializeField]
        private float distanceH = 0.0f;
        [SerializeField]
        private float distanceV = 5.0f;

        //speed the platform moves at
        [SerializeField]
        private float speed = 5.0f;


        void Start()
        {
            Vector3 movement = new Vector3(distanceH, distanceV, 0);
            StartPos = transform.position;
            EndPos = transform.position + movement;
        }

        void FixedUpdate()
        {
            if (!returning)
            {
                transform.position = Vector3.MoveTowards(transform.position, EndPos, speed * Time.deltaTime);

                if (transform.position == EndPos)
                {
                    returning = true;
                }
            }

            if (returning)
            {
                transform.position = Vector3.MoveTowards(transform.position, StartPos, speed * Time.deltaTime);

                if (transform.position == StartPos)
                {
                    returning = false;
                }
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            //parent player to platform
            if (collision.GetComponent<Rigidbody2D>() == PlayerController.rb2D)
            {
                collision.transform.parent = transform;
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            //unparent player
            if (collision.GetComponent<Rigidbody2D>() == PlayerController.rb2D)
            {
                collision.transform.parent = null;
            }
        }
    }
}