# FeatureManagerFramework

Ein leichtgewichtiges, dynamisches Feature-Management-Framework für .NET-Projekte mit Fokus auf A/B-Testing, Szenariensteuerung und flexibler Erweiterbarkeit.

## Hauptkomponenten

- `IFeature`: Schnittstelle für dynamisch austauschbare Features
- `FeatureTagAttribute`: Attribut zur Kennzeichnung und Priorisierung
- `FeatureContext`: Kontextinformation für Szenarien
- `FeatureManager`: Zentrale Feature-Erkennung und Instanziierung
- `FeatureFallbackChain`: Fallback-Logik zur robusten Auswahl
- `FeatureAuditService`: Logging/Auditing der Feature-Nutzung

## Beispiel

```csharp
var context = new FeatureContext("BetaTest");
var manager = new FeatureManager();

var chain = new FeatureFallbackChain(manager, context)
                .AddFallback("FeatureC")
                .AddFallback("FeatureB")
                .AddFallback("FeatureA");

var feature = chain.Resolve();

if (feature != null)
{
    FeatureAuditService.LogFeatureUsage(feature.GetType().Name, context.ActiveScenario);
    feature.Execute();
}
```

## Lizenz

Frei zur Nutzung und Erweiterung.
