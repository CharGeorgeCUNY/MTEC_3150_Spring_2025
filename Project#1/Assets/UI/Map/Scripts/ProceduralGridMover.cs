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

    GridGraph gridGraph;
    Vector3 lastCenterPosition;
    bool isScanning;

    void Start()
    {
        AstarPath.active.Scan();
        CenterGraphOnTarget();
        lastCenterPosition = player.position;
    }

    void Update()
    {
        float distFromCenter = Vector3.Distance(player.position, lastCenterPosition);
        if (distFromCenter > updateDistance)
        {
            CenterGraphOnTarget();
            lastCenterPosition = player.position;
        }
    }

    void CenterGraphOnTarget()
    {
        if (gridGraph == null) { return; }

        Vector3 newCenter = new Vector3(player.position.x, player.position.y, gridGraph.center.z);
        gridGraph.center = newCenter;

        if (autoUpdateGraphOnRecenter) { UpdateGraphAroundCenter(); }
    }

    void UpdateGraphAroundCenter()
    {
        if (isScanning) { return; }

        if (usePartialUpdate)
        {
            float halfWidth = (gridGraph.width * gridGraph.nodeSize) * 0.5f;
            float halfDepth = (gridGraph.depth * gridGraph.nodeSize) * 0.5f;

            Vector3 boxSize = new Vector3((halfWidth * 2) + partialUpdateMargin,
                                    (halfDepth * 2) + partialUpdateMargin, 10f);

            Bounds guoBounds = new Bounds(gridGraph.center, boxSize);
            GraphUpdateObject guo = new GraphUpdateObject(guoBounds);

            AstarPath.active.UpdateGraphs(guo);
        }
        else
        {
            if (useAsyncFullScan) { StartCoroutine(ScanGridAsync()); }
            else { AstarPath.active.Scan(); }
        }
    }

    IEnumerator ScanGridAsync()
    {
        isScanning = true;

        var scanProgress = AstarPath.active.ScanAsync(gridGraph);

        foreach (var progress in scanProgress)
        {
            yield return null;
        }

        isScanning = false;
    }
}
