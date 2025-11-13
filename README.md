# LogTraceMetric

## İçindekiler

- [Observability nedir?](#observability-nedir)
- [Log + Trace + Metric](#log--trace--metric)
- [OpenTelemetry](#opentelemetry)
- [Jaeger ve Zipkin nedir?](#jaeger-ve-zipkin-nedir)
- [Temel Kavramlar](#temel-kavramlar)
  - [Log nedir?](#log-nedir)
  - [Tracing nedir?](#tracing-nedir)
  - [Log ve Trace Farkı](#log-ve-trace-farkı)
  - [Resource nedir?](#resource-nedir)
  - [ActivitySource nedir?](#activitysource-nedir)
  - [Span nedir?](#span-nedir)
  - [Activity (Span) Kind nedir?](#activity-span-kind-nedir)
  - [Event nedir?](#event-nedir)
  - [Activity (Span) Status nedir?](#activity-span-status-nedir)
  - [Tag nedir?](#tag-nedir)
  - [Correlations (In-Process)](#correlations-in-process)

## Observability nedir?

Observability (Gözlemlenebilirlik), .NET uygulamanızın iç durumunu dışarıdan gözlemleyebilme yeteneğidir. `ILogger`, `Activity` ve `Meter` gibi .NET API'lerini kullanarak sistemin davranışını, performansını ve sağlığını izlemek için kullanılır.

## Log + Trace + Metric

.NET'te Log (`ILogger`), Trace (`System.Diagnostics.Activity`) ve Metric (`System.Diagnostics.Metrics.Meter`) observability'nin üç temel sütununu oluşturur. Örnek: `logger.LogInformation("User logged in")` log kaydı, `Activity.Start()` trace başlatır, `meter.CreateCounter<int>("requests")` metrik toplar.


## OpenTelemetry

OpenTelemetry .NET SDK, `System.Diagnostics` API'lerini kullanarak telemetri verilerini toplar ve export eder. NuGet paketleri (`OpenTelemetry.Exporter.Jaeger`, `OpenTelemetry.Exporter.Zipkin`) ile Jaeger, Zipkin gibi backend'lere gönderilir. Örnek: `services.AddOpenTelemetry().WithTracing(builder => builder.AddJaegerExporter())`

## Jaeger ve Zipkin nedir?

Jaeger ve Zipkin, .NET uygulamalarından gelen `Activity` verilerini görselleştiren trace platformlarıdır. .NET'te `OpenTelemetry.Exporter.Jaeger` veya `OpenTelemetry.Exporter.Zipkin` NuGet paketleri ile entegre edilir. Örnek: `builder.AddJaegerExporter(options => options.Endpoint = new Uri("http://localhost:14268/api/traces"))`

## Temel Kavramlar

### Log nedir?
.NET'te log, `ILogger` interface'i ile yapılır. `logger.LogInformation("Processing request")`, `logger.LogError(ex, "Error occurred")` gibi çağrılarla olaylar kaydedilir. Serilog, NLog gibi provider'lar ile dosyaya, veritabanına veya cloud servislere yazılabilir.

### Tracing nedir?
.NET'te tracing, `System.Diagnostics.Activity` sınıfı ile yapılır. `ActivitySource` ile başlatılan `Activity` nesneleri, bir işlemin baştan sona tüm adımlarını takip eder. Örnek: `using var activity = activitySource.StartActivity("ProcessOrder")` ile bir işlem izlenmeye başlar.

### Log ve Trace Farkı
.NET'te log (`ILogger`) bağımsız mesajlardır: `logger.LogInformation("User logged in")`. Trace (`Activity`) ise yapılandırılmış bir süreçtir: `using var activity = activitySource.StartActivity("Login")` ile başlar, `activity.Stop()` ile biter ve tüm adımları zaman damgası ile kaydeder.

### Resource nedir?
.NET'te Resource, OpenTelemetry ile telemetri verilerinin kaynağını tanımlar. Örnek: `ResourceBuilder.CreateDefault().AddService("MyService", "1.0.0").AddAttributes(new[] { new KeyValuePair<string, object>("host.name", Environment.MachineName) })` ile servis adı, versiyon ve host bilgisi eklenir.

### ActivitySource nedir?
.NET'te `ActivitySource`, `Activity` nesnelerini oluşturan fabrika sınıfıdır. Örnek: `var activitySource = new ActivitySource("MyCompany.MyService")` ile oluşturulur, `activitySource.StartActivity("OperationName")` ile yeni activity başlatılır. Aynı source'dan gelen activity'ler birlikte gruplandırılır.

### Span nedir?
.NET'te Span, `Activity` sınıfı ile temsil edilir. `using var span = activitySource.StartActivity("DatabaseQuery")` ile başlar, `span.Stop()` ile biter. Her span, süreyi, ilişkileri ve iç adımları kaydeder. OpenTelemetry'de Activity otomatik olarak Span'e dönüştürülür.

### Activity (Span) Kind nedir?
.NET'te `ActivityKind` enum'u ile belirlenir: `ActivityKind.Server` (gelen istek), `ActivityKind.Client` (giden istek), `ActivityKind.Internal` (iç işlem). Örnek: `activitySource.StartActivity("HttpRequest", ActivityKind.Server)` ile server-side activity oluşturulur.

### Event nedir?
.NET'te `Activity.AddEvent()` ile span içine event eklenir. Örnek: `activity.AddEvent(new ActivityEvent("Cache miss", tags: new ActivityTagsCollection { { "key", "user:123" } }))` ile span zaman çizelgesinde önemli anlar işaretlenir.

### Activity (Span) Status nedir?
.NET'te `Activity.SetStatus()` ile belirlenir: `ActivityStatusCode.Ok` (başarılı), `ActivityStatusCode.Error` (hata), `ActivityStatusCode.Unset` (belirlenmemiş). Örnek: `activity.SetStatus(ActivityStatusCode.Error, "Database connection failed")` ile hata durumu işaretlenir.

### Tag nedir?
.NET'te `Activity.SetTag()` ile key-value çiftleri eklenir. Örnek: `activity.SetTag("http.method", "GET")`, `activity.SetTag("user.id", userId)`. Bu tag'ler OpenTelemetry ile export edilir ve Jaeger/Zipkin'de filtreleme için kullanılır.

### Correlations (In-Process)
.NET'te aynı process içinde `Activity` nesneleri otomatik olarak parent-child ilişkisi kurar. `Activity.Current` ile mevcut activity'ye erişilir, yeni activity başlatıldığında otomatik olarak parent olur. Örnek: `using var child = activitySource.StartActivity("ChildOperation")` mevcut activity'nin child'ı olur.