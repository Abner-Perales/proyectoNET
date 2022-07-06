using GymManager.Core.MembershipTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManager.Web.Models
{
    public class MembershipTypeListViewModel
    {
        public int NewMembershipTypesCount { get; set; }

        public List<MembershipType> MembershipTypes { get; set; }
    }
}
