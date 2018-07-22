using LandAreaResolver.Logger.Interfaces;
using LandAreaResolver.Logger.Loggers;

namespace LandAreaResolver.Logger.Helpers
{ 
    public static class LoggerProviderFactory
    {
        public static ILogger GetProvider(LoggingProviders provider)
        {
            switch (provider)
            {
                case LoggingProviders.Console:
                    return new ConsoleLogger();
                case LoggingProviders.File:
                    return new FileLogger();
                default:
                    return new ConsoleLogger();                    
            }
        }

    }
}
