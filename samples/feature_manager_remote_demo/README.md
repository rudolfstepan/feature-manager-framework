# ProcessFeatureDemo

Dieses Demo-Projekt zeigt, wie Business-Prozesse als remote-konfigurierbare Features implementiert werden können. 

## Architektur
- **Attribute** definieren Default-Implementierungen und Prioritäten.
- **RemoteFeatureManager** lädt JSON-Konfigurationen per HTTP.
- **Fallback** auf lokale Implementierungen, wenn das Backend nicht erreichbar ist.

## Nutzung
1. Backend starten, das JSON-Konfigurationen bereitstellt.
2. `dotnet run` im Projektverzeichnis.
3. Beispiel-Workflow „OrderWorkflow“ wird ausgeführt.

## Beispiel-Konfiguration
Siehe `config/OrderWorkflow.json`.
