using UnityEngine;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;

public class ProceduralGridMover : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public AstarPath aStar;

    [Header("Settings")]
    public float updateDistance = 10f;
    public bool usePartialUpdate = true;
    public bool useAsyncFullScan = false;
    public float partialUpdateMargin = 2f;

    public bool autoUpdateGraphOnRecenter = true;

    private GridGraph gridGraph;
    private Vector3 lastCenterPosition;
    private bool isScanning;

    void Start()
    {
        gridGraph = AstarPath.active.data.gridGraph;

        if (gridGraph == null)
        {
            Debug.LogError("GridGraph is null! Creating a new one.");
            gridGraph = AstarPath.active.data.AddGraph(typeof(GridGraph)) as GridGraph;
        }

        // Make sure the graph is fully initialized
        if (gridGraph != null)
        {
            AstarPath.active.Scan();
            CenterGraphOnTarget();
            lastCenterPosition = player.position;
        }
        else
        {
            Debug.LogError("Failed to initialize GridGraph!");
        }
    }

    void Update()
    {
        if (gridGraph == null)
        {
            Debug.LogError("GridGraph is null in Update.");
            return; // Don't proceed if the graph is not initialized.
        }

        // Check if the player has moved far enough to require a graph update
        float distFromCenter = Vector3.Distance(player.position, lastCenterPosition);
        if (distFromCenter > updateDistance)
        {
            CenterGraphOnTarget();  // Recenter the graph
            lastCenterPosition = player.position;
        }
    }

    void CenterGraphOnTarget()
    {
        if (gridGraph == null)
        {
            Debug.LogError("GridGraph is null while centering.");
            return;
        }

        // Update the center of the grid graph based on the player's position
        Vector3 newCenter = new Vector3(player.position.x, player.position.y, gridGraph.center.z);
        gridGraph.center = newCenter;

        // Optionally, update the graph if necessary
        if (autoUpdateGraphOnRecenter)
        {
            UpdateGraphAroundCenter();
        }
    }

    void UpdateGraphAroundCenter()
    {
        if (isScanning) { return; }

        // Update the graph using partial update if needed
        if (usePartialUpdate)
        {
            // Calculate the area to update around the center of the grid graph
            float halfWidth = (gridGraph.width * gridGraph.nodeSize) * 0.5f;
            float halfDepth = (gridGraph.depth * gridGraph.nodeSize) * 0.5f;

            // Define the bounds for the graph update, adding the margin
            Vector3 boxSize = new Vector3((halfWidth * 2) + partialUpdateMargin,
                                          (halfDepth * 2) + partialUpdateMargin, 10f);

            Bounds guoBounds = new Bounds(gridGraph.center, boxSize);
            GraphUpdateObject guo = new GraphUpdateObject(guoBounds);

            AstarPath.active.UpdateGraphs(guo);
        }
        else
        {
            // Perform a full scan or an async scan
            if (useAsyncFullScan)
            {
                StartCoroutine(ScanGridAsync());
            }
            else
            {
                AstarPath.active.Scan();
            }
        }
    }

    IEnumerator ScanGridAsync()
    {
        isScanning = true;

        if (gridGraph == null)
        {
            Debug.LogError("GridGraph is null during async scan.");
            yield break;  // Exit if gridGraph is null
        }

        // Perform the asynchronous scan of the grid graph
        var scanProgress = AstarPath.active.ScanAsync(gridGraph);

        foreach (var progress in scanProgress)
        {
            yield return null;  // Yield execution until the scan is done
        }

        isScanning = false;
    }
}
