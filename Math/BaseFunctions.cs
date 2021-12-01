namespace Math;

public static class BaseFunctions
{
    public static float H00(Clamp01 t)
    {
        var t2 = t * t;
        var value = 2f * t2 * t - 3f * t2 + 1f;

        return value;
    }

    public static float dH00dt(Clamp01 t) =>
        6f * t * t - 6f * t;

    public static float ddH00ddt(Clamp01 t) =>
        12f * t - 6f;

    public static float EvaluateSupportFunction(System.Numerics.Vector2 position, Support support)
    {
        Vector2InUnitCircle difference = (support.position - position) / support.influence;

        var distance = difference.vector.Length();
        var height = support.size * H00(distance);

        return height;
    }

    public static System.Numerics.Vector2 EvaluateSupportFunctionGradient(System.Numerics.Vector2 position, Support support)
    {
        Vector2InUnitCircle difference = (support.position - position) / support.influence;

        var distance = difference.vector.Length();
        var gradient =
            new System.Numerics.Vector2
                ( 6f * difference.vector.X * distance - 6f * difference.vector.X
                , 6f * difference.vector.Y * distance - 6f * difference.vector.Y
                );

        return gradient;
    }

    public static (System.Numerics.Vector3 tangentX, System.Numerics.Vector3 tangentY) EvaluateSupportFunctionTangents(System.Numerics.Vector2 position, Support support)
    {
        Vector2InUnitCircle difference = (support.position - position) * support.influence;

        var distance =
            System.Math.Clamp(difference.vector.Length(), 0f, 1f);
        var tangentX =
            new System.Numerics.Vector3
                ( 1f
                , 0f
                , 6f * difference.vector.X * distance - 6f * difference.vector.X
                );
        var tangentY =
            new System.Numerics.Vector3
                ( 0f
                , 1f
                , 6f * difference.vector.Y * distance - 6f * difference.vector.Y
                );

        return (tangentX, tangentY);
    }

    public static System.Numerics.Vector3 EvaluateSupportFunctionNormal(System.Numerics.Vector2 position, Support support)
    {
        var (tangentX, tangentY) = EvaluateSupportFunctionTangents(position, support);
        var normal =
            System.Numerics.Vector3.Normalize(System.Numerics.Vector3.Cross(tangentX, tangentY));

        return normal;
    }
}
