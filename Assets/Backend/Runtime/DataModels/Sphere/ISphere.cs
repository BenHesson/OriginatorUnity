using OriginatorKids.Parser;
using System.Collections.Generic;

namespace OriginatorKids.DataModels
{
    public interface ISphere : IParsable
    {
        List<Polygon> Polygons { get; }
        List<Polygon> GetPolygonNeighbors(Polygon polygon);
        List<Polygon> GetPolygonsInSector(int sector);
    }
}
