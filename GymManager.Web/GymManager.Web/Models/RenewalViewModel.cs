using GymManager.Core.Members;
using GymManager.Core.MembershipTypes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymManager.Web.Models
{
    public class RenewalViewModel
    {
        
        public Member Member { get; set; }

        public List<MembershipType> MembershipTypes { get; set; }
        public int IdMembershipType { get; set; }

        public int IdMember { get; set; }
    }
}
