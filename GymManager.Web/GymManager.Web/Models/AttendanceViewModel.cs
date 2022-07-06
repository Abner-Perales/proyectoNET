using GymManager.Core.Attendance;
using GymManager.Core.Members;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymManager.Web.Models
{
    public class AttendanceViewModel
    {
        public int Id { get; set; }

        [BindProperty, DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateCheck { get; set; }

        public Member Member { get; set; }

        public int CheckType { get; set; }

        public int IdMember { get; set; }

        public string Message { get; set; }
    }
}
