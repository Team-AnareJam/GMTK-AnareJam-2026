using UnityEngine;

public static class MathAE
{
    public static float RemapFloat(float value, float initialMin, float initialMax, float targetMin, float targetMax)
    {
        float t = MathAE.InverseLerpUnclamped(initialMin, initialMax, value);
        return Mathf.LerpUnclamped(targetMin, targetMax, t);
    }

    public static float RemapFloatClamped(float value, float initialMin, float initialMax, float targetMin, float targetMax)
    {
        float t = MathAE.InverseLerpUnclamped(initialMin, initialMax, value);
        return Mathf.Clamp(Mathf.LerpUnclamped(targetMin, targetMax, t), targetMin, targetMax);
    }

    public static float InverseLerpUnclamped(float rangeMin, float rangeMax, float value)
    {
        return (value - rangeMin) / (rangeMax - rangeMin);
    }

    public static (Vector2 start, Vector2 end) SwipePositions(Vector2 direction, float angle, float distance)
    {
        direction.Normalize();
        var pos1 = Quaternion.Euler(0, 0, angle) * direction;
        var pos2 = Quaternion.Euler(0, 0, -angle) * direction;
        return (pos1, pos2);
    }
}
