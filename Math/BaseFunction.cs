namespace Math;

public static class BaseFunction
{
    public static float F(Clamp01 t) =>
        2 * t * t * t - 3 * t * t + 1;

    public static float dFdt(Clamp01 t) =>
        6 * (t - 1) * t;

    public static float ddFddt(Clamp01 t) =>
        12 * t - 6;
}
