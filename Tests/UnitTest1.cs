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
            Math.BaseFunctions.SupportTangents
                (new Vector2(0f, 0f), support);

        var left =
            Math.BaseFunctions.SupportTangents
                (new Vector2(-1f, 0f), support);

        var halfLeft =
            Math.BaseFunctions.SupportTangents
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
            Math.BaseFunctions.SupportTangents
                (path.First(), support);
        var projectedTangent =
            System.Numerics.Vector3.Normalize(jacobianStart.tangentY);
        var tangents =
            new List<Vector3>();

        tangents.Add(projectedTangent);

        foreach(var position in path.Skip(1))
        {
            var currentNormal =
                Math.BaseFunctions.SupportNormal
                    (position, support);
            projectedTangent =
                System.Numerics.Vector3.Normalize
                    (Math.BaseFunctions.Project(currentNormal, projectedTangent));

            tangents.Add(projectedTangent);
        }

        Assert.Pass();
    }

    [Test]
    public void ParallelTransportShooting()
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
            new List<Vector2>
                ( new [] { new Vector2(.5f, -1f) } );
        var currentVelocity = new Vector2(0f, 1f);
        var startingJacobian =
            Math.BaseFunctions.SupportTangents(path.First(), support);
        var currentTangent =
            startingJacobian.tangentY;

        do
        {
            var newPosition =
                path.Last() + currentVelocity * .1f;
            var newJacobian =
                Math.BaseFunctions.SupportTangents
                    ( newPosition, support );
            var newNormal =
                Math.BaseFunctions.SupportNormal
                    ( newPosition, support );
            currentTangent =
                Math.BaseFunctions.Project(newNormal, currentTangent);
            // alten
            currentVelocity =
                Math.BaseFunctions.ChangeBase(newJacobian, currentTangent);

            path.Add(newPosition);
        } while(path.Count() < 100);
    }
}