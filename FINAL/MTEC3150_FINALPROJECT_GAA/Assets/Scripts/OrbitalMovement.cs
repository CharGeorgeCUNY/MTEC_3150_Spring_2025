using UnityEngine;
using System.Collections.Generic;

//Relatively complex script developed using quaternions to control orbit around objects stored in a listed array.

public class OrbitalMovement : MonoBehaviour
{
    public float minOrbitSpeed = 10f;
    public float maxOrbitSpeed = 40f;
    public float minOrbitRadius = 2f;
    public float maxOrbitRadius = 5f;

    private class OrbitingBody
    {
        public Transform transform;
        public Vector3 axis;
        public float speed;
        public float radius;
        public Vector3 offset;
    }

    private List<OrbitingBody> orbiters = new List<OrbitingBody>();
    private Transform center;

    void Start()
    {
        List<GameObject> allObjects = new List<GameObject>();
        allObjects.AddRange(GameObject.FindGameObjectsWithTag("randomObject"));
        allObjects.AddRange(GameObject.FindGameObjectsWithTag("Planet"));

        if (allObjects.Count == 0) return;

        // First object becomes the center of orbit
        center = allObjects[0].transform;

        for (int i = 1; i < allObjects.Count; i++)
        {
            Transform t = allObjects[i].transform;
            Vector3 randomAxis = Random.onUnitSphere;
            float speed = Random.Range(minOrbitSpeed, maxOrbitSpeed);
            float radius = Random.Range(minOrbitRadius, maxOrbitRadius);
            Vector3 offset = (t.position - center.position).normalized * radius;

            orbiters.Add(new OrbitingBody
            {
                transform = t,
                axis = randomAxis,
                speed = speed,
                radius = radius,
                offset = offset
            });
        }
    }

    void Update()
    {
        foreach (var orbiter in orbiters)
        {
            // Rotate the offset around the axis
            Quaternion rotation = Quaternion.AngleAxis(orbiter.speed * Time.deltaTime, orbiter.axis);
            orbiter.offset = rotation * orbiter.offset;

            // Apply new position
            orbiter.transform.position = center.position + orbiter.offset;
        }
    }
}
