# Remote-gesteuerte Prozessfeatures

Diese Dokumentation beschreibt das Konzept und die Implementierung:

1. **Attribute**: 
   - `ProcessFeatureAttribute` markiert Workflow-Klassen.
   - Parameter: Key, Priority, DefaultEnabled.

2. **RemoteFeatureManager**:
   - Lädt Konfigurationen per HTTP.
   - Wählt Workflow basierend auf Conditions und Priority.

3. **Conditions**:
   - `CustomerTier`, `Region`, `TimeWindow`

4. **Fallback**:
   - Lokale Default-Implementierungen per Attribut.
   - Sicherstellung der Basis-Funktionalität.

5. **Live-Reload** (Ausblick):
   - WebSocket/SignalR für Push-Updates.
