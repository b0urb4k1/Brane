using System.Diagnostics;

namespace Math;

public class NonNegativeFloat
{
    private readonly float value;
    [Conditional("Debug")]
    private void Validate(float value)
    {
        if (value < 0.0f) throw new ArgumentException("value smaller than zero.");
    }
    public NonNegativeFloat(float value)
    {
        Validate(value);

        this.value = value;
    }
}
