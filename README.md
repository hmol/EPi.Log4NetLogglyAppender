Log4net Loggly Appender for EPiServer CMS
=======================

A simple log4net appender for sending log entries to [Loggly.com](http://www.loggly.com) for EPiServer CMS.

# Dependencies

* [log4net 1.2.10](https://www.nuget.org/packages/log4net/1.2.10)
* [Json.NET 4.0.1+](https://www.nuget.org/packages/Newtonsoft.Json/4.0.1)
* [Microsoft .NET Framework 4 HTTP Client Libraries](https://www.nuget.org/packages/Microsoft.Net.Http/2.0.20505)

# Installation

1. Install the package via NuGet.

2. Replace the endpoint URL and token value with your own Loggly endpoint URL and token in EPiServerLog.config.

3. Profit. Your application should now transmit everything you log using log4net to your Loggly.com feed.

# Important

* Tags are only valid for Loggly API Generation 2 upwards. So, if you're using the old Loggly API (Generation 1), DO NOT include any tags. Doing so will result in a request error and your log entry will not be recorded.