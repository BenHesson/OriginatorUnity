using OriginatorKids.DataModels;
using OriginatorKids.Parser.Exceptions;
using System;

namespace OriginatorKids.Parser.PolygonalSphere
{
    /// <summary>
    /// Acts as an intermediary so that we can distinguish between versions of the sphere data
    /// </summary>
    internal class SphereParser
    {
        public ISphere Create(string data)
        {
            //We'll just split up the file and check the first line for the version here

            string[] sphereData = data.Split(
                new string[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            );

            if (sphereData == null || sphereData.Length < 2)
                throw new ParseException($"Can't parse sphere. Not enough data.");

            string versionLine = sphereData[0];

            AbstractDataParser<ISphere> parser;

            switch (versionLine)
            {
                case "v.1":
                    parser = new Sphere_v1();
                    break;
                default:
                    throw new ParseException("Can't parse sphere. Unable to determine which file version is being used");
            }

            return parser.Parse(sphereData);
        }

    }
}
