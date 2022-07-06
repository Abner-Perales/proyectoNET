using GymManager.Core.Members;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.Core.Attendance
{
    public class Check
    {
        [Key]
        public int Id { get; set; }

        [BindProperty, DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateCheck { get; set; }

        public Member Member { get; set; }

        public string CheckType { get; set; }
    }
}
