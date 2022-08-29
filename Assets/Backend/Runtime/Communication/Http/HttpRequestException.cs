using System;
using UnityEngine.Networking;

namespace OriginatorKids.Communication.Http.Exceptions
{
    public class HttpRequestException : Exception
    {
        public int HttpStatusCode { get; } = -1;
        public string RawResponseData { get; } = "";
        public string Url { get; } = "";

        public HttpRequestException(UnityWebRequest request)
            : base ($"Http Request Exception with status code: {(int)request.responseCode} while sending to {request.url}{Environment.NewLine}{request.downloadHandler.text}")
        {
            HttpStatusCode = (int)request.responseCode;
            RawResponseData = request.downloadHandler.text;
            Url = request.url;
        }

        public override string ToString()
        {
            return $"Http Request Exception with status code: {this.HttpStatusCode} while sending to {this.Url}{Environment.NewLine}{this.RawResponseData}";
        }
    }
}