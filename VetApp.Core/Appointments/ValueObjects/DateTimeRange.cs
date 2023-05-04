using System;
using VDMJasminka.Core.Appointments.Exceptions;

namespace VDMJasminka.Core.Appointments.ValueObjects
{
    public class DateTimeRange
    {
        public DateTimeRange(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new DateTimeRangeStartDatePreceedsEndException($"Start date for range {start} is bigger than end date {end}");
            }

            Start = start;
            End = end;
        }

        private DateTimeRange()
        {

        }

        public DateTime Start { get; }
        public DateTime End { get; }
    }
}
