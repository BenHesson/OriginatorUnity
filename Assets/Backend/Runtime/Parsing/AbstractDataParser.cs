using OriginatorKids.Parser.Exceptions;
using UnityEngine;

namespace OriginatorKids.Parser
{
    internal abstract class AbstractDataParser<T>
    {
        /// <summary>
        /// Receives the lines of the data file and parses them in order to create whatever type of IParsable was passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract T Parse(string[] data);

        /// <summary>
        /// Helper method to convert a commma seperated string to a Unity Vector3
        /// </summary>
        /// <param name="stringData"></param>
        /// <returns></returns>
        internal Vector3 StringToVector3(string stringData)
        {
            string[] split = stringData.Split(',');

            if (split.Length < 2)
                throw new ParseException($"Parse exception converting StringToVector3. Not enough variables to convert. Data: {stringData}");

            float x, y, z;

            if (!float.TryParse(split[0], out x))
                throw new ParseException($"Parse exception converting StringToVector3. X variable: {split[0]} unable to convert to a float");

            if (!float.TryParse(split[1], out y))
                throw new ParseException($"Parse exception converting StringToVector3. Y variable: {split[1]} unable to convert to a float");

            if (!float.TryParse(split[2], out z))
                throw new ParseException($"Parse exception converting StringToVector3. Z variable: {split[2]} unable to convert to a float");

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Helper method to convert a commma seperated string to an int array
        /// </summary>
        /// <param name="stringData"></param>
        /// <returns></returns>
        internal int[] StringToIntArray(string stringData)
        {
            string[] split = stringData.Split(',');

            int[] result = new int[split.Length];

            for (int i = 0; i < split.Length; i++)
            {
                if (!int.TryParse(split[i], out result[i]))
                    throw new ParseException($"Parse exception converting StringToIntArray. Character index: {i} in string: {stringData}");
            }
            return result;
        }
    }

}