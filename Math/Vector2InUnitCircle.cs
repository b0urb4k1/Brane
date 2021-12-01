using System.Diagnostics;

namespace Math;

public class Vector2InUnitCircle
{
    public System.Numerics.Vector2 vector;

    [Conditional("Debug")]
    private void Validate(System.Numerics.Vector2 vector)
    {
        if (IsInUnitCircle(vector) is false)
            throw new ArgumentException("vector too long");
    }

    public bool IsInUnitCircle(System.Numerics.Vector2 vector) =>
        vector.Length() > 1.0f ? false : true;

    public Vector2InUnitCircle(System.Numerics.Vector2 vector)
    {
        Validate(vector);

        this.vector = vector;
    }

    public static implicit operator System.Numerics.Vector2(Vector2InUnitCircle value) =>
        value.vector;
    public static implicit operator Vector2InUnitCircle(System.Numerics.Vector2 value) =>
        new Vector2InUnitCircle(value);
}
