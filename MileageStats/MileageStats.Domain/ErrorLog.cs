using System;
using Microsoft.Practices.ServiceLocation;

namespace MileageStats.Domain
{
    public static class ErrorLog
    {
        public static void Log(Exception exception)
        {
            var errorLogger = ServiceLocator.Current.GetInstance<IErrorLogger>();
            errorLogger.Log(exception);
        }
    }
}