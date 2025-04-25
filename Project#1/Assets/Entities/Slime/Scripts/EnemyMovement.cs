using Pathfinding;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    public Transform player;
    public float nextWaypointDist = 0.5f;
    public Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    private Vector2 lastPosition;
    private float stuckThreshold = 0.5f; // The distance threshold for being considered stuck

    Seeker seeker;
    Rigidbody2D rb;
    Animator animator; // Reference to the Animator

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Get the Animator component
        player = FindObjectOfType<PlayerMovement>().transform;

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone()) { seeker.StartPath(rb.position, player.position, OnPathComplete); }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null) { return; }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else { reachedEndOfPath = false; }

        // Direction to the next waypoint
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, nextWaypointDist);

        if (hit.collider != null)
        {
            direction = Vector2.Perpendicular(direction);
        }

        Vector2 force = direction * enemyData.MoveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + force);

        // Update animation parameters
        UpdateAnimation(direction);

        // Check if we are close enough to the next waypoint
        float dist = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (dist < nextWaypointDist) { currentWaypoint++; }
    }

    // Function to update animation based on movement direction
    void UpdateAnimation(Vector2 direction)
    {
        if (direction.magnitude > 0.1f)
        {
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
        }
    }
}
