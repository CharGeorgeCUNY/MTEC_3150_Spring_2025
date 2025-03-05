using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptableObject", menuName = "ScriptableObjects/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    [SerializeField] GameObject startingWeapon;
    public GameObject StartingWeapon { get => StartingWeapon; private set => startingWeapon = value; }

    [SerializeField] float maxHealth;
    public float MaxHealth { get => maxHealth ; private set => maxHealth = value; }
    [SerializeField] float recovery;
    public float Recovery { get => recovery ; private set => recovery = value; }
    [SerializeField] float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }
    [SerializeField] float attack;
    public float Attack { get => attack; private set => attack = value; }
    [SerializeField] float projectileSpeed;
    public float ProjectileSpeed { get => projectileSpeed; private set => projectileSpeed = value; }
}
