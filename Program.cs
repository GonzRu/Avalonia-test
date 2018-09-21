using System;
using Avalonia;
using Avalonia.Logging.Serilog;
using avalonia_test2.ViewModels;
using avalonia_test2.Views;

namespace avalonia_test2
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildAvaloniaApp().Start<MainWindow>(() => new MainWindowViewModel());
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI()
                .LogToDebug();
    }
}
