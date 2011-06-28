using System;
using MileageStats.Domain;

namespace MileageStats.Web
{
    internal class ErrorLogger : IErrorLogger
    {
        #region IErrorLogger Members

        void IErrorLogger.Log(Exception exception)
        {
            try
            {
                var logEntry = new LogEntry
                                   {
                                       Date = DateTime.Now,
                                       Message = exception.Message,
                                       StackTrace = exception.StackTrace,
                                   };

                using (var datacontext = new LogDBDataContext())
                {
                    datacontext.LogEntries.InsertOnSubmit(logEntry);
                    datacontext.SubmitChanges();
                }
            }
            catch (Exception)
            {
                // failed to record exception
            }
        }

        #endregion
    }
}
