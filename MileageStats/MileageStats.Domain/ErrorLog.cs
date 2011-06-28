using System;
using Microsoft.Practices.ServiceLocation;

namespace MileageStats.Domain
{
    public static class ErrorLog
    {
        public static void Log(Exception exception)
        {
            if (ServiceLocator.Current == null)
            {
                return; // It could be null in a test environment
            }

            var errorLogger = ServiceLocator.Current.GetInstance<IErrorLogger>();
            errorLogger.Log(exception);
        }
    }
}
