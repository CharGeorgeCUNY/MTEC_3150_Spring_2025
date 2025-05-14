using UnityEngine;

public class HazardAutoScroll : MonoBehaviour
{
    // grab this from your background scroller or PlayerMovement
    PlayerMovement pm;

    void Awake()
    {
        
    }

    void Update()
    {
        if (pm == null)
        {
            pm = FindObjectOfType<PlayerMovement>();
        }
        else if (pm != null)
        {
            // move left at the same speed your player would have been moving forward
            float speed = pm.CurrentSpeedX;
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
}
