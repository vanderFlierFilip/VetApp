using VDMJasminka.Core.Appointments.ScheduleAggregate;
using VDMJasminka.Core.Common;

namespace VDMJasminka.Core.Appointments.Events
{
    public class AppointmentAddedToSchedule : IDomainEvent
    {
        public AppointmentAddedToSchedule(Schedule schedule, Appointment appointment)
        {
            Schedule = schedule;
            Appointment = appointment;
        }

        public Schedule Schedule { get; }
        public Appointment Appointment { get; }

    }
}
