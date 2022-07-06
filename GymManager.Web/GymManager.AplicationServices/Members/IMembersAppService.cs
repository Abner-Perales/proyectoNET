using GymManager.Core.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.AplicationServices.Members
{
    public interface IMembersAppService
    {

        Task<List<Member>> GetMembersAsync();

        Task<List<Member>> GetMembersAsync(int count);

        Task<List<Member>> GetMembersFilterAsync(string filter);

        Task<int> AddMemberAsync(Member member);

        Task DeleteMemberAsync(int memberId);

        Task<Member> GetMemberAsync(int memberId);

        Task EditMemberAsync(Member member);

    }
}
