using System;
using UnityEngine;

namespace WinterSym
{
    public class UtilityFlags : MonoBehaviour
    {
        [SerializeField] public GroundFlagType GroundFlag;
    }


    //categorized enums containing various flags
    public enum GroundFlagType
    {
        none = 0,
        Ignore = 1,
        Ice = 2
    }
}