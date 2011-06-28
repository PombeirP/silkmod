using System;

namespace MileageStats.Domain
{
    public interface IErrorLogger
    {
        void Log(Exception exception);
    }
}
