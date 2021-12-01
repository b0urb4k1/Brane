namespace Math;

public class Clamp01
{
    private readonly float t;

    public Clamp01(float t)
    {
        this.t = System.Math.Clamp(t, 0.0f, 1.0f);
    }

    public static implicit operator float(Clamp01 value) =>
        value.t;
    public static implicit operator Clamp01(float value) =>
        new Clamp01(value);
}
