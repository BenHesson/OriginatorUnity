using System;
using NUnit.Framework;
using OriginatorKids.Communication.Http;
using OriginatorKids.DataModels;
using OriginatorKids.Parser.Exceptions;
using OriginatorKids.Parser.PolygonalSphere;
using UnityEngine.Networking;

public class Tests
{
    [Test]
    public void TestParsingPentagon()
    {
        var versionedParser = new Sphere_v1();

        var pentagonDataClean = "0 0 1 0,39,0 1.138,38.888,1.567 1.842,38.888,-0.598 0,38.888,-1.936 -1.842,38.888,-0.598 -1.138,38.888,1.567 1,2,3,4,5";

        Polygon pentagonPoly = versionedParser.CreatePolygon(pentagonDataClean,0);

        Assert.AreEqual(pentagonPoly.PolygonType, Polygon.PolygonSideType.Pentagon);

        Assert.AreEqual(pentagonPoly.RingNumber, 0);
        Assert.AreEqual(pentagonPoly.RingPosition, 0);
        Assert.AreEqual(pentagonPoly.SectorNumber, 1);

        Assert.AreEqual(pentagonPoly.CanonicalCenter.x, 0);
        Assert.AreEqual(pentagonPoly.CanonicalCenter.y, 39);
        Assert.AreEqual(pentagonPoly.CanonicalCenter.z, 0);

        Assert.AreEqual(pentagonPoly.VertexPoints.Count, 5);
        Assert.AreEqual(pentagonPoly.Neighbors.Length, 5);


        var hexagonDataClean = "1 0 1 3.125,38.872,1.015 1.138,38.888,1.567 2.312,38.768,3.181 4.494,38.635,2.696 5.221,38.635,0.46 3.74,38.768,-1.215 1.842,38.888,-0.598 5,6,7,8,2,0";

        Polygon hexagonPoly = versionedParser.CreatePolygon(hexagonDataClean,1);

        Assert.AreEqual(hexagonPoly.PolygonType, Polygon.PolygonSideType.Hexagon);

        Assert.AreEqual(hexagonPoly.RingNumber, 1);
        Assert.AreEqual(hexagonPoly.RingPosition, 0);
        Assert.AreEqual(hexagonPoly.SectorNumber, 1);

        Assert.AreEqual(hexagonPoly.CanonicalCenter.x, 3.125f);
        Assert.AreEqual(hexagonPoly.CanonicalCenter.y, 38.872f);
        Assert.AreEqual(hexagonPoly.CanonicalCenter.z, 1.015f);

        Assert.AreEqual(hexagonPoly.VertexPoints.Count, 6);
        Assert.AreEqual(hexagonPoly.Neighbors.Length, 6);
    }

    [Test]
    public void TestParsingVector3()
    {
        var parser = new Sphere_v1();
        var vector3 = parser.StringToVector3("1,2,3");
        Assert.AreEqual(vector3.x, 1);
        Assert.AreEqual(vector3.y, 2);
        Assert.AreEqual(vector3.z, 3);
    }

    [Test]
    public void TestParsingIntArray()
    {
        var parser = new Sphere_v1();
        var array = parser.StringToIntArray("1,2,3");
        Assert.AreEqual(array[0], 1);
        Assert.AreEqual(array[1], 2);
        Assert.AreEqual(array[2], 3);
        Assert.AreEqual(array.Length, 3);
    }

    [Test]
    public void TestSphereVersionParsing()
    {
        var cleanData = $"A.B{Environment.NewLine}0 0 1 0,39,0 1.138,38.888,1.567 1.842,38.888,-0.598 0,38.888,-1.936 -1.842,38.888,-0.598 -1.138,38.888,1.567 1,2,3,4,5";
        
        SphereParser sphereParser = new SphereParser();
        try
        {
            sphereParser.Create(cleanData);
            Assert.Fail("Expected ParseException when using an unallowed version number");
        }
        catch(ParseException)
        {
            Assert.Pass();
        }
        catch(Exception)
        {
            Assert.Fail("Expected ParseException when using an unallowed version number");
        }
    }

    [Test]
    public void TestSphereHelpers()
    {
        SphereParser sphereParser = new SphereParser();
        var cleanData = @"v.1
0 0 1 0,39,0 1.138,38.888,1.567 1.842,38.888,-0.598 0,38.888,-1.936 -1.842,38.888,-0.598 -1.138,38.888,1.567 1,2,3,4,5
1 0 1 3.125,38.872,1.015 1.138,38.888,1.567 2.312,38.768,3.181 4.494,38.635,2.696 5.221,38.635,0.46 3.74,38.768,-1.215 1.842,38.888,-0.598 5,6,7,8,2,0
1 1 1 1.931,38.872,-2.658 1.842,38.888,-0.598 3.74,38.768,-1.215 3.954,38.635,-3.44 2.051,38.635,-4.822 0,38.768,-3.932 0,38.888,-1.936 1,8,9,10,3,0
1 2 1 -1.931,38.872,-2.658 -1.842,38.888,-0.598 0,38.888,-1.936 0,38.768,-3.932 -2.051,38.635,-4.822 -3.954,38.635,-3.44 -3.74,38.768,-1.215 0,2,10,11,12,4
1 3 1 -3.125,38.872,1.015 -1.138,38.888,1.567 -1.842,38.888,-0.598 -3.74,38.768,-1.215 -5.221,38.635,0.46 -4.494,38.635,2.696 -2.312,38.768,3.181 0,3,12,13,14,5
1 4 1 0,38.872,3.286 1.138,38.888,1.567 -1.138,38.888,1.567 -2.312,38.768,3.181 -1.176,38.635,5.107 1.176,38.635,5.107 2.312,38.768,3.181 0,4,14,15,6,1";

        ISphere sphere = sphereParser.Create(cleanData);

        Assert.AreEqual(sphere.Polygons.Count, 6);
        Assert.AreEqual(sphere.GetPolygonNeighbors(sphere.Polygons[0])[0], sphere.Polygons[1]);
        Assert.AreEqual(sphere.GetPolygonsInSector(1).Count, 6);
    }

    [Test]
    public void TestWebRequestCreation()
    {
        string url = "http://originatorkids.com/";
        var httpCommunicator = new HttpCommunicator(url);
        httpCommunicator.Timeout = 99;
        httpCommunicator.HttpRequestType = HttpCommunicator.RequestType.GET;

        UnityWebRequest request = httpCommunicator.SetupWebRequest();
        Assert.AreEqual(request.url,url);
        Assert.AreEqual(request.method, HttpCommunicator.RequestType.GET.ToString());
        Assert.AreEqual(request.timeout, 99);
    }
}
