using VDMJasminka.Core.Appointments.ValueObjects;
using VDMJasminka.Core.Common;

namespace VDMJasminka.Core.Appointments.ScheduleAggregate
{
    public class Appointment : Entity
    {
        private Appointment()
        {

        }
        public Appointment(int ownerId, int petId, AppointmentType appointmentType, DateTimeRange range)
        {
            OwnerId = ownerId;
            PetId = petId;
            AppointmentType = appointmentType;
            Range = range;
        }

        public override int Id { get; protected set; }
        public int OwnerId { get; }
        public int PetId { get; }
        public int ScheduleId { get; set; }
        public AppointmentType AppointmentType { get; }
        public DateTimeRange Range { get; private set; }

        public void ChangeTimeRange(DateTimeRange startEnd)
        {
            Range = startEnd;
        }

    }
}
