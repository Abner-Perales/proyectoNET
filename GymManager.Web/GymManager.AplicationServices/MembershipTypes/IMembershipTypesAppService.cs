using GymManager.Core.MembershipTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.AplicationServices.MembershipTypes
{
    public interface IMembershipTypesAppService
    {
        Task<List<MembershipType>> GetMembershipTypesAsync();

        Task<int> AddMembershipTypeAsync(MembershipType membershiptype);

        Task DeleteMembershipTypeAsync(int membershipTypeId);

        Task<MembershipType> GetMembershipTypeAsync(int membershipTypeId);
        Task EditMembershipTypeAsync(MembershipType membershiptype);
    }
}
