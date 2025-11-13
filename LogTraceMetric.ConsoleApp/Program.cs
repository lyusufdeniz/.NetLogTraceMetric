using LogTraceMetric.ConsoleApp;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
//create tracer provider
var traceProvider = Sdk.CreateTracerProviderBuilder().
    ConfigureResource(conf => conf.AddService(OpenTelemetryConstant.ServiceName, serviceVersion: OpenTelemetryConstant.ServiceVersion).
    AddAttributes(new List<KeyValuePair<string,object>>()
    {
        // seperate words with dot(.) not underscore(_) e.g. host.machineName
        new KeyValuePair<string, object>("host.machineName", Environment.MachineName),
        new KeyValuePair<string, object>("host.environment", "dev"),

    })).Build();