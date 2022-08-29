using System.Threading.Tasks;
using UnityEngine.Networking;
using OriginatorKids.Communication.Http.Exceptions;

namespace OriginatorKids.Communication.Http
{
    /// <summary>
    /// An object that facilitates simple web requests
    /// </summary>
    public class HttpCommunicator
    {
        //This class is a small wrapper around the UnityWebRequest. If we wanted to get rid of the dependency on the UnityEngine.Networking
        //We could implment this using the normal .NET HTTP classes but it will require a bit more effort since Unity gives us a lot right
        //out of the box.

        //only GET for now, but we can easily add others and expand this as needed
        public enum RequestType
        {
            GET
        }

        readonly string URL;

        public RequestType HttpRequestType = RequestType.GET;

        public int Timeout = 30;

        public HttpCommunicator(string url)
        {
            URL = url;
        }

        /// <summary>
        /// Sends the webrequest to the server
        /// </summary>
        /// <returns>A string representation of the result payload</returns>
        public async Task<string> Send()
        {
            using (UnityWebRequest request = SetupWebRequest())
            {
                request.SendWebRequest();

                while (!request.isDone)
                {
                    await Task.Yield();
                }

                switch (request.result)
                {
                    case UnityWebRequest.Result.Success:
                        return request.downloadHandler.text;
                    default:
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                    case UnityWebRequest.Result.ProtocolError:
                        throw new HttpRequestException(request);
                }
            }
        }

        internal UnityWebRequest SetupWebRequest()
        {
            UnityWebRequest request = new UnityWebRequest();

            //set url
            request.url = URL;

            //set method type. We could also do a switch case here, but this s nice especially if we add more http verbs
            request.SetRequestHeader("X-HTTP-Method-Override", HttpRequestType.ToString());

            //add a timeout
            request.timeout = Timeout;

            //setup a download handler for the response
            request.downloadHandler = new DownloadHandlerBuffer();

            return request;
        }
    }
}