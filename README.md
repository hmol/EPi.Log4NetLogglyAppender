Log4net Loggly Appender
=======================

A simple log4net appender for sending log entries to [Loggly.com](http://www.loggly.com).

# Dependencies

* [log4net 1.2.10](https://www.nuget.org/packages/log4net/1.2.10)
* [Json.NET 4.0.1+](https://www.nuget.org/packages/Newtonsoft.Json/4.0.1)

# Installation

Install the package via NuGet.

After installing the NuGet package, you will need to register the appender in your Web.config file. Please replace the token value with your own Loggly token.

```xml
<log4net>
    <appender name="loggly" type="Log4NetLogglyAppender.Appender, Log4NetLogglyAppender">
        <endpoint value="REPLACE-THIS-WITH-YOUR-LOGGLY-ENDPOINT"/>
        <token value="REPLACE-THIS-WITH-YOUR-LOGGLY-TOKEN"/>
        <tags>tag1,tag2</tags>
    </appender>

    <root>
        <level value="Warn"/>

        <appender-ref ref="loggly"/>
    </root>
</log4net>
```

That's it. Your application should now transmit everything you log using log4net to your Loggly.com feed.

# Notes

* Tags are only valid for Loggly API Generation 2 upwards. So, if you're using the old Loggly API (Generation 1), DO NOT include any tags. Doing so will result in a request error and your log entry will not be recorded.