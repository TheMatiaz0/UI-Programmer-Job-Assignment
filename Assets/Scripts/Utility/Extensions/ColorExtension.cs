using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorExtension
{
    public static float GetLuma(this Color color)
    {
        // Rec. 709 colour primaries.
        const float r = 0.2126f;
        const float g = 0.7152f;
        const float b = 0.0722f;

        return color.r * r + color.g * g + color.b * b;
    }

    public static bool IsColorDark(this Color color)
    {
        return GetLuma(color) < 0.5f || color.a < 0.5f;
    }
}
