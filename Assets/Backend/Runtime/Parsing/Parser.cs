using OriginatorKids.DataModels;
using OriginatorKids.Parser.Exceptions;
using OriginatorKids.Parser.PolygonalSphere;

namespace OriginatorKids.Parser
{
    /// <summary>
    /// Entry object for parsing IParsable objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Parser<T> where T : IParsable
    {
        /// <summary>
        /// Uses the string data passed in then constructs and returns an object that is of type IParsable.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>An object of type IParsable that can then be cast to the appropriate type</returns>
        public IParsable Parse(string data)
        {
            var type = typeof(T);
            //As new parsable types are needed, we can add them here
            if (type == typeof(ISphere)) return new SphereParser().Create(data);

            throw new ParseException($"Can't find the appropriate type to use for IParsable. Maybe the implementation needs to be added");
        }
    }
}