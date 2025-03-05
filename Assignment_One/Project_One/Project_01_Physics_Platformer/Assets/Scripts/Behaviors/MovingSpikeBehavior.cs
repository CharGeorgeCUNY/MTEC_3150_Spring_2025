using UnityEngine;
using UnityEngine.UIElements;

namespace PhysicsSym
{
    public class MovingSpikeBehavior : MonoBehaviour
    {
        //fixed variables

        //where the platform moves from and to
        private Vector3 StartPos, EndPos;

        //is the platform returning to its starting position?
        private bool returning = false;

        //editor variables

        //marker to move to
        [SerializeField]
        private GameObject marker;

        //speed the platform moves at
        [SerializeField]
        private float speed = 5.0f;


        void Start()
        {
            StartPos = transform.position;
            EndPos = marker.transform.position;
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
    }
}