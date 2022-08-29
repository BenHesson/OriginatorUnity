using OriginatorKids.DataModels;
using OriginatorKids.Parser.Exceptions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace OriginatorKids.Parser.PolygonalSphere
{
    internal class Sphere_v1 : AbstractDataParser<ISphere>
    {
        public override ISphere Parse(string[] data)
        {
            List<Polygon> polygons = new List<Polygon>();

            //skip the first line since it is just the version number
            for(int i = 1; i < data.Length; i++)
            {
                polygons.Add(CreatePolygon(data[i],i));
            }

            return new Sphere(polygons);
        }

        internal Polygon CreatePolygon(string data, int line)
        {
            //Split the data by spaces
            string[] lineData = data.Split(' ');
            //check for the proper length
            if (lineData.Length < 10 || lineData.Length > 11)
                throw new ParseException($"Parse exception improper formatting of line data. Data: {data} on line: {line}");

            //parse ring number
            int ringNumber;
            if (!int.TryParse(lineData[0], out ringNumber))
                throw new ParseException($"Parse exception cannot parse the ring number. Ring Number Data: {lineData[0]}");

            //parse ring position
            int ringPosition;
            if (!int.TryParse(lineData[1], out ringPosition))
                throw new ParseException($"Parse exception cannot parse the ring position. Ring Number Data: {lineData[1]}");

            //parse sectorNumber
            int sectorNumber;
            if (!int.TryParse(lineData[2], out sectorNumber))
                throw new ParseException($"Parse exception cannot parse the sector number. Ring Number Data: {lineData[2]}");

            //Parse canonicalCenter
            Vector3 canonicalCenter = Vector3.zero;
            try
            {
                canonicalCenter = StringToVector3(lineData[3]);
            }
            catch (Exception e)
            {
                throw new ParseException($"Parse exception: {e.Message} on line: {line}", e);
            }

            //Parse first 5 or 6 vertices depending on if it's a hexagon or pentagon
            List<Vector3> vertices = new List<Vector3>();
            for (int ii = 4; ii < ((lineData.Length == 10) ? 9 : 10); ii++)
            {
                try
                {
                    vertices.Add(StringToVector3(lineData[ii]));
                }
                catch (Exception e)
                {
                    throw new ParseException($"Parse exception: {e.Message} on line: {line}", e);
                }
            }

            //parse the neighbor indices
            int[] neighbors;
            try
            {
                neighbors = StringToIntArray(lineData[((lineData.Length == 10) ? 9 : 10)]);

            }
            catch (Exception e)
            {
                throw new ParseException($"Parse exception: {e.Message} on line: {line}", e);
            }

            if (neighbors.Length != ((lineData.Length == 10) ? 5 : 6))
                throw new ParseException($"Parse exception neighbor length needs to be {((lineData.Length == 10) ? 5 : 6)}, but it is: {neighbors.Length}");

            //build our polygon object
            return new Polygon()
            {
                RingNumber = ringNumber,
                RingPosition = ringPosition,
                SectorNumber = sectorNumber,
                CanonicalCenter = canonicalCenter,
                VertexPoints = vertices,
                Neighbors = neighbors,
                PolygonType = ((lineData.Length == 10) ? Polygon.PolygonSideType.Pentagon : Polygon.PolygonSideType.Hexagon)
            };
        }

    }
}
