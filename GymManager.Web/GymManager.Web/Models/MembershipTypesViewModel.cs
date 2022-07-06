﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymManager.Web.Models
{
    public class MembershipTypesViewModel
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Enter valid cost.")]
        [DataType(DataType.Currency)]
        public double Cost { get; set; }

        [BindProperty, DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Enter valid duration.")]
        public int Duration { get; set; }

    }
}
