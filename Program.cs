using Avalonia;
using Avalonia.Logging.Serilog;
using Avalonia_test.ViewModels;
using Avalonia_test.Views;

namespace Avalonia_test
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
