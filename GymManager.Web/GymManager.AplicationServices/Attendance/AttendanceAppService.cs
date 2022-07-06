using GymManager.Core.Attendance;
using GymManager.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.AplicationServices.Attendance
{
    public class AttendanceAppService : IAttendanceAppService
    {
        private readonly IRepository<int, Check> _repository;

        public AttendanceAppService(IRepository<int, Check> repository)
        {
            _repository = repository;
        }

        public async Task<int> AddCheckAsync(Check check)
        {
            await _repository.AddAsync(check);

            return check.Id;
        }

        public async Task DeleteCheckAsync(int checkId)
        {
            await _repository.DeleteAsync(checkId);
        }

        public async Task EditCheckAsync(Check check)
        {
            await _repository.UpdateAsync(check);
        }

        public async Task<Check> GetCheckAsync(int checkId)
        {
            return await _repository.GetAsync(checkId);
        }

        public async Task<List<Check>> GetChecksAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }

    }
}
