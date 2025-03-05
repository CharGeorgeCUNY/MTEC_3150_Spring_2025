using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowController : WeaponController
{
    public AudioSource audioSource;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedBolt = Instantiate(weaponData.Prefab);
        spawnedBolt.transform.position = transform.position;
        spawnedBolt.GetComponent<CrossbowBehavior>().DirectionChecker(pm.lastMovement);
        audioSource.Play();
    }
}
