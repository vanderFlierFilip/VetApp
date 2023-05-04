using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDMJasminka.Core.Appointments.ScheduleAggregate;

namespace VDMJasminka.Core.Interfaces
{
    public interface IScheduleRepository
    {
        Task<IEnumerable<Schedule>> GetAll();
        Task<Schedule> GetScheduleWithRangeAsync(int scheduleId, DateTime from, DateTime to);
        Task Update(Schedule entity);
    }
}