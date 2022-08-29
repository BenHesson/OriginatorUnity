using System.Collections.Generic;
using UnityEngine;

namespace OriginatorKids.DataModels
{
    public class Polygon
    {
        public enum PolygonSideType
        {
            Pentagon,
            Hexagon
        }

        public PolygonSideType PolygonType { get; internal set; }
        public int RingNumber { get; internal set; }
        public int RingPosition { get; internal set; }
        public int SectorNumber { get; internal set; }
        public Vector3 CanonicalCenter { get; internal set; }
        public List<Vector3> VertexPoints { get; internal set; }
        public int[] Neighbors { get; internal set; }
    }
}
