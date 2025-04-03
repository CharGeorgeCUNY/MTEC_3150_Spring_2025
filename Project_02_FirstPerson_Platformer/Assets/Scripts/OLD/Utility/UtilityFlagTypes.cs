using System;
using System.Collections.Generic;
using UnityEngine;

namespace JumpSym.Utility
{
    //categorized enums containing various flags
    public enum ObstacleFlagType
    {
        none = 0,
        Wall = 1,
        Fence = 2,
        Beam = 3,
        Grip = 4
    }

    public enum IgnoreGroundFlagType
    {
        none = 0,
        Ignore = 1
    }
}