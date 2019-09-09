using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexMetrics
{
    public const float outerRadius = 1f;
    public const float innerRadius = outerRadius * outerToInner;

    public const float outerToInner = 0.866025404f;
    public const float innerToOuter = 1f / outerToInner;
}
