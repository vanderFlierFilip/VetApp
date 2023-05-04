using System;
using System.Collections.Generic;
using System.Linq;
using VDMJasminka.Core.Appointments.Events;
using VDMJasminka.Core.Appointments.Exceptions;
using VDMJasminka.Core.Appointments.ValueObjects;
using VDMJasminka.Core.Common;

namespace VDMJasminka.Core.Appointments.ScheduleAggregate
{
    public class Schedule : Entity, IAggregateRoot
    {
        private Schedule()
        {

        }
        public Schedule(int id, List<Appointment> appointments)
        {
            Id = id;
            _appointments = new List<Appointment>(appointments);
        }

        public override int Id { get; protected set; }

        public IEnumerable<Appointment> Appointments => _appointments.AsReadOnly();
        private List<Appointment> _appointments = new List<Appointment>();

        public void AddAppointment(int ownerId, int petId, DateTime start, DateTime end, string appointmentType)
        {
            var range = new DateTimeRange(start, end);
            var type = new AppointmentType(appointmentType);
            var appointment = new Appointment(ownerId, petId, type, range);

            _appointments.Add(appointment);

            AddDomainEvent(new AppointmentAddedToSchedule(this, appointment));
        }

        public void RemoveAppointment(int appointmentId)
        {
            var appointment = GetAppointmentFromList(appointmentId);

            _appointments.Remove(appointment);

            AddDomainEvent(new AppointmentCancelled(this, appointment));
        }

        public void ChangeAppointmentTime(int appointmentId, DateTime newStart, DateTime newEnd)
        {
            var appointment = GetAppointmentFromList(appointmentId);
            var range = new DateTimeRange(newStart, newEnd);

            appointment.ChangeTimeRange(range);

            AddDomainEvent(new AppointmentTimeChanged(appointment));

        }

        private Appointment GetAppointmentFromList(int id)
        {
            var appointment = _appointments.FirstOrDefault(appointment => appointment.Id == id);

            if (appointment == null)
            {
                throw new AppointmentDoesNotExistInCurrentScheduleException($"Appointment with id {id} is not in current schedule with id {Id}");
            }

            return appointment;
        }


    }


}
