using System.Diagnostics;
using Serilog;
using ILogger = NCA.Common.Application.Infrastructure.Log.ILogger;

namespace NCA.Common.Infrastructure.Log
{
    public class Logger : ILogger
    {
        public Logger()
        {
            //Serilog.Log.Logger = new LoggerConfiguration()
            //    // set default minimum level
            //    .MinimumLevel.Information()
            //    // add console as target
            //    .WriteTo.Console()
            //    // add console as target
            //    .WriteTo.Debug()
            //    .CreateLogger();
        }

        public void LogVerbose(string message)
        {
            Debug.WriteLine(message);
            Serilog.Log.Verbose(message);
        }

        public void LogDebug(string message)
        {
            Debug.WriteLine(message);
            Serilog.Log.Debug(message);
        }

        public void LogInformation(string message)
        {
            Debug.WriteLine(message);
            Serilog.Log.Information(message);
        }

        public void LogWarning(string message)
        {
            Debug.WriteLine(message);
            Serilog.Log.Warning(message);
        }

        public void LogError(string message)
        {
            Debug.WriteLine(message);
            Serilog.Log.Error(message);
        }

        public void LogFatal(string message)
        {
            Debug.WriteLine(message);
            Serilog.Log.Fatal(message);
        }
    }
}
