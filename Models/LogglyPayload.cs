using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EPi.Log4NetLogglyAppender.Models
{
    [Serializable]
    public class LogglyPayload
    {
        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("time")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime Time { get; set; }

        [JsonProperty("machine")]
        public string Machine { get; set; }

        [JsonProperty("process")]
        public string Process { get; set; }

        [JsonProperty("thread")]
        public string Thread { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("logger")]
        public string Logger { get; set; }

        [JsonProperty("exception")]
        public LogglyException Exception { get; set; }

        [JsonProperty("request")]
        public LogglyRequest Request { get; set; }
    }

    [Serializable]
    public class LogglyRequest
    {
        public LogglyRequest(HttpRequest request)
        {
            if (request != null)
            {
                HostName = request.UserHostAddress;
                Url = request.Url.PathAndQuery;
                HttpMethod = request.HttpMethod;
            }
        }

        [JsonProperty("hostname")]
        public string HostName { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("httpmethod")]
        public string HttpMethod { get; set; }

        [JsonProperty("ipaddress")]
        public string IpAddress { get; set; }

        [JsonProperty("querystring")]
        public string QueryString { get; set; }

        [JsonProperty("cookies")]
        public List<KeyValuePair<string, string>> Cookies { get; set; }
    }

    [Serializable]
    public class LogglyException
    {
        public LogglyException(Exception exception)
        {
            if (exception != null)
            {
                Message = exception.Message;
                Type = exception.GetType().Name;
                StackTrace = exception.StackTrace;

                if (exception.InnerException != null)
                {
                    InnerException = new LogglyInnerException(exception.InnerException);
                }
            }
        }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("stacktrace")]
        public string StackTrace { get; set; }

        [JsonProperty("innerexception")]
        public LogglyInnerException InnerException { get; set; }
    }

    [Serializable]
    public class LogglyInnerException
    {
        public LogglyInnerException(Exception exception)
        {
            Message = exception.Message;
            Type = exception.GetType().Name;
            StackTrace = exception.StackTrace;
        }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("stacktrace")]
        public string StackTrace { get; set; }
    }
}
