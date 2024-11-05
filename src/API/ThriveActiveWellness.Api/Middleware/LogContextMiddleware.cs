﻿using System.Diagnostics;
using Serilog.Context;

namespace ThriveActiveWellness.Api.Middleware;

internal sealed class LogContextMiddleware(RequestDelegate next)
{
    public Task Invoke(HttpContext context)
    {
        string traceId = Activity.Current?.TraceId.ToString();

        using (LogContext.PushProperty("TraceId", traceId))
        {
            return next.Invoke(context);
        }
    }
}