using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Net.Http;
using log4net.Appender;
using log4net.Core;
using Newtonsoft.Json;

namespace Log4NetLogglyAppender
{
    public class Appender : AppenderSkeleton
    {
        private readonly Process _currentProcess = Process.GetCurrentProcess();
        private string _endpoint;

        /// <summary>
        /// Loggly endpoint for submitting log events.
        /// </summary>
        public string Endpoint
        {
            get { return _endpoint; }
            set
            {
                _endpoint = value;
                if (!_endpoint.EndsWith("/"))
                    _endpoint += "/";
                if (!_endpoint.EndsWith("inputs/"))
                    _endpoint += "inputs/";
            }
        }

        /// <summary>
        /// Loggly customer token.
        /// https://www.loggly.com/docs/customer-token-authentication-token/
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Comma-delimited list of tags to add to each log event.
        /// Relevant only for Loggly API generation 2+
        /// https://www.loggly.com/docs/tags/
        /// </summary>
        public string Tags { get; set; }

        public Appender() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tags">Collection of additional tags to add to each log event.</param>
        public Appender(IEnumerable<string> tags)
        {
            if (!string.IsNullOrWhiteSpace(Tags))
                Tags += ",";

            Tags += string.Join(",", tags);
        }

        /// <summary>
        /// Appender itself
        /// </summary>
        /// <param name="loggingEvent">The internal representation of logging events.</param>
        protected override void Append(LoggingEvent loggingEvent)
        {
            dynamic payload = new ExpandoObject();
            payload.level = loggingEvent.Level.DisplayName;
            payload.time = loggingEvent.TimeStamp.ToString("o");
            payload.machine = Environment.MachineName;
            payload.process = _currentProcess.ProcessName;
            payload.thread = loggingEvent.ThreadName;
            payload.message = loggingEvent.RenderedMessage;
            payload.logger = loggingEvent.LoggerName;

            var exception = loggingEvent.ExceptionObject;
            if (exception != null)
            {
                payload.exception = new ExpandoObject();
                payload.exception.message = exception.Message;
                payload.exception.type = exception.GetType().Name;
                payload.exception.stackTrace = exception.StackTrace;
                if (exception.InnerException != null)
                {
                    payload.exception.innerException = new ExpandoObject();
                    payload.exception.innerException.message = exception.InnerException.Message;
                    payload.exception.innerException.type = exception.InnerException.GetType().Name;
                    payload.exception.innerException.stackTrace = exception.InnerException.StackTrace;
                }
            }

            var client = new HttpClient();
            var url = string.Format("{0}{1}{2}", Endpoint, Token, string.IsNullOrWhiteSpace(Tags) ? string.Empty : "/tag/" + Tags);
            var payloadJson = JsonConvert.SerializeObject(payload);

            client.PostAsync(url, new StringContent(payloadJson));
        }
    }
}
