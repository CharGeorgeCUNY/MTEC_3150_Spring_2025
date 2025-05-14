using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [Tooltip("Horizontal scroll speed multiplier")]
    public float speedX = 0.5f;
    [Tooltip("Vertical scroll speed multiplier")]
    public float speedY = 0.2f;

    private PlayerMovement playerMovement;
    [SerializeField] private Renderer bgRenderer;

    void Update()
    {
        if (playerMovement == null)
        {
            var playerGO = GameObject.FindWithTag("Player");
            if (playerGO != null)
                playerMovement = playerGO.GetComponent<PlayerMovement>();
        }
        if (playerMovement == null) return;

        // horizontal velocity
        float vx = playerMovement.CurrentSpeedX;
        // vertical velocity (positive = up, negative = falling)
        float vy = playerMovement.CurrentSpeedY;

        // Move X by vx, and Y opposite to vy (so when you fall, bg scrolls up)
        Vector2 delta = new Vector2(
            vx * speedX * Time.deltaTime,
           -vy * speedY * Time.deltaTime
        );
        bgRenderer.material.mainTextureOffset += delta;
    }
}