﻿using NLog;
using NLog.Config;
using NLog.Targets;

namespace S203.uManage
{
    internal static class Logger
    {
        internal static void StartLogging()
        {
            var config = new LoggingConfiguration();
            var consoleTarget = new ColoredConsoleTarget();
            var fileTarget = new FileTarget();

            config.AddTarget("console", consoleTarget);
            config.AddTarget("file", fileTarget);
            
            consoleTarget.Layout = "${date:format=yyyy-MM-dd HH\\:MM\\:ss} ${level:upperCase=true} ${message}";
            fileTarget.FileName = "${basedir}/logs/umanage-server.log";
            fileTarget.Layout = "${date:format=yyyy-MM-dd HH\\:MM\\:ss} ${level:upperCase=true} ${message} ${exception:format=tostring}";
            fileTarget.ArchiveFileName = "${basedir}/logs/umanage-server.{#####}.log";
            fileTarget.ArchiveAboveSize = 20971520;  // 20 MB
            fileTarget.ArchiveNumbering = ArchiveNumberingMode.Rolling;

            // TODO: These need to be configurable by the user otherwise the logs get freaking huge!
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, consoleTarget));
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, fileTarget));

            LogManager.Configuration = config;
        }
    }
}
