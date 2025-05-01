# FeatureManagerFramework

Ein leichtgewichtiges, dynamisches Feature-Management-Framework für .NET-Projekte mit Fokus auf A/B-Testing, Szenariensteuerung und flexible Erweiterbarkeit.

## 🔧 Hauptkomponenten

- **`IFeature`**: Schnittstelle für dynamisch austauschbare Features.
- **`FeatureTagAttribute`**: Attribut zur Kennzeichnung und Priorisierung von Features.
- **`FeatureContext`**: Kontextinformationen für Szenarien, z. B. Benutzergruppen oder Umgebungen.
- **`FeatureManager`**: Zentrale Komponente zur Erkennung und Instanziierung von Features.
- **`FeatureFallbackChain`**: Implementiert eine Fallback-Logik zur robusten Auswahl von Features.
- **`FeatureAuditService`**: Ermöglicht Logging und Auditing der Feature-Nutzung.

## 🚀 Einstieg

### Voraussetzungen

- .NET 6.0 oder höher

### Installation

Fügen Sie das Projekt Ihrem Solution-Ordner hinzu oder integrieren Sie es als Submodul:

```bash
git submodule add https://github.com/rudolfstepan/feature-manager-framework.git
```


### Beispielverwendung


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
    feature.Execute();
}
```


## 🧪 Beispielprojekt

Im Ordner `samples/SampleConsoleApp` befindet sich eine Beispielanwendung, die die Verwendung des Frameworks demonstriert.

## 📁 Projektstruktur

- **`src/FeatureManager`**: Enthält die Hauptkomponenten des Frameworks.
- **`samples/SampleConsoleApp`**: Beispielanwendung zur Demonstration der Framework-Funktionalitäten.
- **`.gitignore`**: Definiert Dateien und Ordner, die von der Versionskontrolle ausgeschlossen sind.
- **`README.md`**: Diese Dokumentation.

## 📌 Lizenz

Dieses Projekt steht unter der MIT-Lizenz. Weitere Informationen finden Sie in der [LICENSE](https://github.com/rudolfstepan/feature-manager-framework/blob/main/LICENSE)-Datei.

## 🤝 Beitrag leisten

Beiträge sind willkommen! Bitte öffnen Sie ein Issue oder einen Pull Request, um Verbesserungen vorzuschlagen oder Fehler zu melden.

---

Für weitere Informationen besuchen Sie bitte das [GitHub-Repository](https://github.com/rudolfstepan/feature-manager-framework/tree/main). 
