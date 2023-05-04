using System;

namespace VDMJasminka.Core.Appointments.Exceptions
{
    public class AppointmentDoesNotExistInCurrentScheduleException : Exception
    {
        public AppointmentDoesNotExistInCurrentScheduleException() : base()
        {
        }

        public AppointmentDoesNotExistInCurrentScheduleException(string message) : base(message)
        {
        }

        public AppointmentDoesNotExistInCurrentScheduleException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
