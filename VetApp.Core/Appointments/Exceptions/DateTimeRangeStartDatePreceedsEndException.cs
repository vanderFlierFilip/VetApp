using System;

namespace VDMJasminka.Core.Appointments.Exceptions
{
    public class DateTimeRangeStartDatePreceedsEndException : Exception
    {
        public DateTimeRangeStartDatePreceedsEndException() : base()
        {
        }

        public DateTimeRangeStartDatePreceedsEndException(string message) : base(message)
        {
        }

        public DateTimeRangeStartDatePreceedsEndException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
