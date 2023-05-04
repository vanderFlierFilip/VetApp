using System;

namespace VDMJasminka.Core.Appointments.ValueObjects
{
    public class AppointmentType
    {
        public string Value { get; }

        private AppointmentType()
        {

        }

        public AppointmentType(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            Value = value;
        }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}
