using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using FeatureManagerFramework.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace FeatureManagerFramework.Providers
{
    /// <summary>
    /// Loads plugins from the "Plugins" directory and watches for changes (Hot-Reload).
    /// </summary>
    public class DirectoryFeatureProvider : IFeatureProvider, IHostedService, IDisposable
    {
        private readonly string _pluginFolder;
        private readonly IServiceProvider _serviceProvider;
        private readonly FileSystemWatcher _watcher;
        private readonly ConcurrentDictionary<string, List<IFeature>> _features = new();

        public DirectoryFeatureProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _pluginFolder = Path.Combine(AppContext.BaseDirectory, "Plugins");
            Directory.CreateDirectory(_pluginFolder);
            _watcher = new FileSystemWatcher(_pluginFolder, "*.dll")
            {
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
            };
            _watcher.Created += OnChanged;
            _watcher.Changed += OnChanged;
            _watcher.Deleted += OnDeleted;

            LoadExistingPlugins();
        }

        private void LoadExistingPlugins()
        {
            foreach (var file in Directory.GetFiles(_pluginFolder, "*.dll"))
                LoadPlugin(file);
        }

        private void LoadPlugin(string path)
        {
            try
            {
                var assembly = Assembly.LoadFrom(path);
                var features = assembly.GetTypes()
                    .Where(t => typeof(IFeature).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                    .Select(t => ActivatorUtilities.CreateInstance(_serviceProvider, t) as IFeature)
                    .Where(f => f != null)
                    .ToList();
                _features[path] = features;
            }
            catch (Exception ex)
            {
                // Optional: add logging
                System.Diagnostics.Debug.WriteLine($"Error loading plugin {path}: {ex.Message}");
            }
        }

        private void OnChanged(object sender, FileSystemEventArgs e) => LoadPlugin(e.FullPath);
        private void OnDeleted(object sender, FileSystemEventArgs e) => _features.TryRemove(e.FullPath, out _);

        public IReadOnlyCollection<IFeature> GetFeatures()
        {
            return _features.Values.SelectMany(x => x).ToList().AsReadOnly();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _watcher.EnableRaisingEvents = true;
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _watcher.EnableRaisingEvents = false;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _watcher.Dispose();
        }
    }
}