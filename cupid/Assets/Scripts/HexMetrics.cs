using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMetrics : MonoBehaviour
{
    public const float OuterRadius = 10f;
    public const float InnerRadius = OuterRadius * 0.866025404f;

    public static Vector3[] Corners =
    {
        new Vector3(0f, 0f, OuterRadius),
        new Vector3(InnerRadius, 0f, 0.5f * OuterRadius),
        new Vector3(InnerRadius, 0f, -0.5f * OuterRadius),
        new Vector3(0f, 0f, -OuterRadius),
        new Vector3(-InnerRadius, 0f, -0.5f * OuterRadius),
        new Vector3(-InnerRadius, 0f, 0.5f * OuterRadius)
    };
    
    


}
