using System;

namespace MileageStats.Domain
{
    public static class ErrorLog
    {
        private static IErrorLogger logger;

        public static void Setup(IErrorLogger errorLogger)
        {
            logger = errorLogger;
        }

        public static void Log(Exception exception)
        {
            if (logger == null)
            {
                return; // It could be null in a test environment
            }

            logger.Log(exception);
        }
    }
}
