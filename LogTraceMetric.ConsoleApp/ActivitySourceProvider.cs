using System.Diagnostics;

namespace LogTraceMetric.ConsoleApp
{
    internal static class ActivitySourceProvider
    {
        public static ActivitySource Source = new ActivitySource(OpenTelemetryConstant.ActivitySourceName);
    }
}
