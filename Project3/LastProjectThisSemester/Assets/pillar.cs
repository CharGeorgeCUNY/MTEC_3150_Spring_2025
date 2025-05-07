using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillar : MonoBehaviour
{
    GameManager gm;
    public int rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponentInParent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.PlanetCanMove)
        {
            float horizontalInput = Input.GetAxis("Horizontal");


            this.transform.localRotation *= Quaternion.Euler(new Vector3(0, 0, -horizontalInput * rotSpeed * Time.deltaTime));
        }

    }
}
