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

Observability (Gözlemlenebilirlik), bir sistemin iç durumunu dışarıdan gözlemleyebilme ve anlayabilme yeteneğidir. Log, Trace ve Metric gibi telemetri verilerini kullanarak sistemin davranışını, performansını ve sağlığını izlemek ve sorunları tespit etmek için kullanılır.

## Log + Trace + Metric

Log, Trace ve Metric, observability'nin üç temel sütununu oluşturur. Log, uygulamanın belirli anlardaki durumunu ve olaylarını kaydeder. Trace, bir işlemin baştan sona tüm adımlarını ve servisler arası ilişkilerini takip eder. Metric ise sistemin performans ölçümlerini, sayısal değerlerini ve istatistiklerini toplar. Bu üçü birlikte kullanıldığında, sistemin tam bir görünümünü sağlar.


## OpenTelemetry

OpenTelemetry, telemetri verilerini (log, trace, metric) toplamak, işlemek ve export etmek için açık kaynaklı, vendor-agnostic bir standart ve araç setidir. Farklı diller ve platformlar için SDK'lar sağlar ve telemetri verilerini çeşitli backend sistemlerine (Jaeger, Zipkin, Prometheus vb.) gönderebilir.

## Jaeger ve Zipkin nedir?

Jaeger ve Zipkin, distributed tracing için kullanılan açık kaynaklı trace görselleştirme ve analiz platformlarıdır. Jaeger, Uber tarafından geliştirilmiş modern bir trace sistemi iken, Zipkin, Twitter tarafından geliştirilmiş ve daha uzun süredir kullanılan bir sistemdir. Her ikisi de trace verilerini toplar, saklar ve görselleştirir, böylece dağıtılmış sistemlerde bir isteğin tüm servisler arasındaki yolculuğunu takip edebilirsiniz.

## Temel Kavramlar

### Log nedir?
Log (Günlük), bir uygulamanın çalışma sırasında gerçekleşen olayları, hataları ve bilgilendirme mesajlarını zaman damgası ile kaydeden kayıt sistemidir. Uygulamanın durumunu izlemek, hataları tespit etmek ve sistem davranışını analiz etmek için kullanılır.

### Tracing nedir?
Tracing (İzleme), bir uygulamanın çalışma sırasında işlemlerin ve isteklerin nasıl ilerlediğini takip etme ve kaydetme sürecidir. Dağıtılmış sistemlerde bir isteğin farklı servisler arasında nasıl ilerlediğini görselleştirmek ve performans sorunlarını tespit etmek için kullanılır.

### Log ve Trace Farkı
Log, uygulamanın belirli anlardaki durumunu ve olaylarını kaydeden bağımsız mesajlardır, genellikle tek bir noktada ne olduğunu gösterir. Trace ise bir işlemin baştan sona tüm adımlarını, span'ler arasındaki ilişkileri ve zamanlamayı içeren yapılandırılmış bir izleme sistemidir; dağıtılmış sistemlerde bir isteğin tüm servisler arasındaki yolculuğunu takip eder.

### Resource nedir?
Resource (Kaynak), telemetri verilerinin toplandığı uygulama veya servisi tanımlayan bilgilerdir. Servis adı, versiyon, host bilgisi gibi metadata içerir ve tüm telemetri verilerinin hangi kaynaktan geldiğini belirler.

### ActivitySource nedir?
ActivitySource, Activity nesnelerini oluşturmak için kullanılan bir fabrika sınıfıdır. Aynı ActivitySource'u kullanan Activity'ler, aynı kaynak adı altında gruplandırılır ve telemetri toplama sistemlerinde birlikte görüntülenir.

### Span nedir?
Span, bir işlemin veya operasyonun başlangıcından bitişine kadar olan süreyi temsil eden temel izleme birimidir. Her span, bir işlemin ne kadar sürdüğünü, hangi adımları içerdiğini ve diğer span'lerle ilişkisini kaydeder.

### Activity (Span) Kind nedir?
Activity Kind, bir span'in sistem içindeki rolünü ve davranışını belirten bir özelliktir. Client, Server, Producer, Consumer, Internal gibi türleri vardır ve span'in isteği başlatan mı yoksa alan mı olduğunu gösterir.

### Event nedir?
Event (Olay), bir span içinde gerçekleşen önemli anları veya durum değişikliklerini temsil eden zaman damgalı kayıtlardır. Span'in zaman çizelgesi üzerinde belirli noktalarda ne olduğunu detaylandırmak için kullanılır.

### Activity (Span) Status nedir?
Activity Status, bir span'in başarılı mı yoksa başarısız mı tamamlandığını gösteren durum bilgisidir. Ok (başarılı), Error (hata) veya Unset (belirlenmemiş) değerlerini alabilir ve hata ayıklama için kritik bilgi sağlar.

### Tag nedir?
Tag (Etiket), bir span'e veya telemetri verisine eklenen key-value çiftleridir. İşlem hakkında ek bağlamsal bilgi sağlar (örneğin HTTP metodu, veritabanı sorgusu, kullanıcı ID'si) ve filtreleme ve sorgulama için kullanılır.

### Correlations (In-Process)
Correlations (Korelasyonlar), aynı işlem içinde farklı Activity'lerin birbirleriyle ilişkilendirilmesi mekanizmasıdır. Parent-child ilişkileri kurarak, bir işlemin farklı bileşenlerinin nasıl birbirine bağlandığını ve hiyerarşik olarak nasıl organize olduğunu gösterir.