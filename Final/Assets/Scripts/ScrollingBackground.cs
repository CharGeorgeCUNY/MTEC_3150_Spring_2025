using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [Tooltip("Assign your Player gameObject (with PlayerMovement on it) here")]
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private Renderer bgRenderer;

    void Update()
    {
        if (playerMovement == null)
        {
            Debug.LogWarning("[ScrollingBackground] No PlayerMovement assigned!");
            return;
        }

        // Grab the player's horizontal speed:
        float playerSpeed = playerMovement.CurrentSpeed;

        // Scroll the texture by exactly that amount (per second):
        bgRenderer.material.mainTextureOffset +=
            new Vector2(playerSpeed * Time.deltaTime, 0f);

        Debug.Log($"[ScrollingBackground] playerSpeed={playerSpeed:F2}, offset.x={bgRenderer.material.mainTextureOffset.x:F2}");
    }
}