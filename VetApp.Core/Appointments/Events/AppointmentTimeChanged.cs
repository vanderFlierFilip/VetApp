using VDMJasminka.Core.Appointments.ScheduleAggregate;
using VDMJasminka.Core.Common;

namespace VDMJasminka.Core.Appointments.Events
{
    public class AppointmentTimeChanged : IDomainEvent
    {
        public Appointment Appointment { get; set; }
        public AppointmentTimeChanged(Appointment appointment)
        {
            Appointment = appointment;
        }
    }
}
