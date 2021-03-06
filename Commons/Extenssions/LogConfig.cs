﻿using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Commons.Extenssions
{
    public static class LogConfig
    {
        public static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration, string appName)
        {
            string processid = Process.GetCurrentProcess().Id.ToString();
            return new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.With(new ThreadIDEnricher(processid))
                //.Enrich.WithProperty("ApplicationContext", AppName)
                //.Enrich.FromLogContext()
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Error)
                .ReadFrom.Configuration(configuration)
                .WriteTo.File($"{appName}/logs/{appName}-{processid}-.log",
                    rollOnFileSizeLimit: true, fileSizeLimitBytes: 1024 * 1024 * 100,
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}][{Level}][{ThreadId}][{ProcessId}] {Message:lj} {NewLine}{Exception}")
                .CreateLogger();
        }
    }

    public class ThreadIDEnricher : ILogEventEnricher
    {
        string _processId;

        public ThreadIDEnricher(string processId)
        {
            _processId = processId;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
              "ThreadId", Thread.CurrentThread.ManagedThreadId.ToString("D4")));
            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(
              "ProcessId", _processId));
        }
    }
}
