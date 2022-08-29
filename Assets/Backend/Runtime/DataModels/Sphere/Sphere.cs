using System.Collections.Generic;
using System.Linq;

namespace OriginatorKids.DataModels
{
    /// <summary>
    /// Base class for our sphere object
    /// </summary>
    public class Sphere : ISphere
    {
        public List<Polygon> Polygons { get; private set; }

        public Sphere(List<Polygon> polygons)
        {
            this.Polygons = polygons;
        }

        /// <summary>
        /// Retrieves the neighboring polygons that share a similar side
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public List<Polygon> GetPolygonNeighbors(Polygon polygon)
        {
            List<Polygon> result = new List<Polygon>();
            foreach(int neighbor in polygon.Neighbors)
            {
                result.Add(Polygons[neighbor]);
            }
            return result;
        }

        /// <summary>
        /// Retrieves the polygons that belong to a given sector
        /// </summary>
        /// <param name="sector"></param>
        /// <returns></returns>
        public List<Polygon> GetPolygonsInSector(int sector)
        {
            return Polygons.Where(a => a.SectorNumber == sector).ToList();
        }
    }
}
