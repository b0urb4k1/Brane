using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestTangents()
    {
        var support =
            new Math.Support
                ( 1f
                , 1f
                , new Vector2
                    ( 0f
                    , 0f
                    )
                );
        var middle =
            Math.BaseFunctions.EvaluateSupportFunctionTangents
                (new Vector2(0f, 0f), support);

        var left =
            Math.BaseFunctions.EvaluateSupportFunctionTangents
                (new Vector2(-1f, 0f), support);

        var halfLeft =
            Math.BaseFunctions.EvaluateSupportFunctionTangents
                (new Vector2(-.5f, 0f), support);
    }

    [Test]
    public void ParallelTransport()
    {
        var support =
            new Math.Support
                ( 1f
                , 1f
                , new Vector2
                    ( 0f
                    , 0f
                    )
                );
        var path =
            Enumerable
            .Range(0, 100)
            .Select(i => i * 1f / 100f)
            .Select(
                t =>
                    Vector2.Lerp
                        ( new Vector2(.5f, -1f)
                        , new Vector2(.5f,  1f)
                        , t
                        )
                    );
        var jacobianStart =
            Math.BaseFunctions.EvaluateSupportFunctionTangents
                (path.First(), support);
        var projectedTangent =
            System.Numerics.Vector3.Normalize(jacobianStart.tangentY);
        var tangents =
            new List<Vector3>();

        tangents.Add(projectedTangent);

        foreach(var position in path.Skip(1))
        {
            var currentNormal =
                Math.BaseFunctions.EvaluateSupportFunctionNormal
                    (position, support);
            projectedTangent =
                System.Numerics.Vector3.Normalize(projectedTangent - System.Numerics.Vector3.Dot(currentNormal, projectedTangent) * currentNormal);

            tangents.Add(projectedTangent);
        }

        Assert.Pass();
    }
}