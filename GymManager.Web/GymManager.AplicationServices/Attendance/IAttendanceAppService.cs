using GymManager.Core.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.AplicationServices.Attendance
{
    public interface IAttendanceAppService
    {
        Task<List<Check>> GetChecksAsync();

        Task<int> AddCheckAsync(Check check);

        Task DeleteCheckAsync(int checkId);

        Task<Check> GetCheckAsync(int checkId);

        Task EditCheckAsync(Check check);

    }
}
