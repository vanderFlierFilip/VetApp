using VDMJasminka.Core.Appointments.ScheduleAggregate;
using VDMJasminka.Core.Common;

namespace VDMJasminka.Core.Appointments.Events
{
    public class AppointmentCancelled : IDomainEvent
    {
        public AppointmentCancelled(Schedule schedule, Appointment appointment)
        {
            Schedule = schedule;
            Appointment = appointment;
        }

        public Schedule Schedule { get; set; }
        public Appointment Appointment { get; set; }
    }
}
