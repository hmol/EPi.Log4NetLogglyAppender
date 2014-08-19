Log4net Loggly Appender
=======================

A simple log4net appender for sending log entries to [Loggly.com](http://www.loggly.com).

# Installation

Install the package via NuGet.

After installing the NuGet package, you will need to register the appender in your Web.config file. Please replace the token value with your own Loggly token.

```xml
<log4net>
    <appender name="loggly" type="Log4NetLogglyAppender.Appender, Log4NetLogglyAppender">
        <endpoint value="https://logs.loggly.com/"/>
        <token value="REPLACE-THIS-WITH-YOUR-LOGGLY-TOKEN"/>
    </appender>

    <root>
        <level value="Warn"/>

        <appender-ref ref="loggly"/>
    </root>
</log4net>

```

That's it. It should transmit everything you log using log4net to your Loggly.com feed.