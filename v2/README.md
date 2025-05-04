# Sample Console App with Plugin & Hot-Reload

## Setup

1. Build the projects:
   ```bash
   dotnet build FeatureManagerFramework/FeatureManagerFramework.csproj
   dotnet build SamplePlugin/SamplePlugin.csproj
   dotnet build SampleApp/SampleApp.csproj
   ```

2. Copy the plugin DLL to the Plugins folder:
   ```bash
   cp SamplePlugin/bin/Debug/net6.0/SamplePlugin.dll SampleApp/Plugins/
   ```

3. Run the sample app:
   ```bash
   dotnet run --project SampleApp/SampleApp.csproj
   ```

4. The app will print the status of `HelloFeature` every 5 seconds.  
   Modify `SamplePlugin` (e.g. change `EvaluateAsync` logic), rebuild, and overwrite the DLL in `SampleApp/Plugins/` to see hot-reload in action.
