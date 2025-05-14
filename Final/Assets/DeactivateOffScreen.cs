using UnityEngine;

public class HazardDeactivateOffScreen : MonoBehaviour
{
    void Update()
    {
        var vp = Camera.main.WorldToViewportPoint(transform.position);
        if (vp.x < -0.1f || vp.y < -0.1f || vp.y > 1.1f)
            gameObject.SetActive(false);
    }
}
