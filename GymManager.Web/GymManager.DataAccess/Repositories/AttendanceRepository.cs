using GymManager.Core.Attendance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.DataAccess.Repositories
{
    public class AttendanceRepository : Repository<int, Check>
    {
        public AttendanceRepository(GymManagerContext context) : base(context)
        {

        }

        public override async Task<Check> GetAsync(int id)
        {
            var check = await Context.Checks.Include(x => x.Member).FirstOrDefaultAsync(x => x.Id == id);
            check = await Context.Checks.Include(x => x.CheckType).FirstOrDefaultAsync(x => x.Id == id);
            return check;
        }

        /*
        public async Task<CheckType> GetCheckTypesAsync(int id)
        {
            var checktype = await Context.CheckTypes.FirstOrDefaultAsync(x => x.Id == id);
            return checktype;
        }
        */
    }
}
