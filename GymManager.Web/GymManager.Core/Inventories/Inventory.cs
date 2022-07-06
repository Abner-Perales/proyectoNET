using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.Core.Inventories
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Enter valid stock.")]
        public int Stock { get; set; }

        [BindProperty, DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Enter valid cost.")]
        [DataType(DataType.Currency)]
        public double Cost { get; set; }

        public MeasureType MeasureType { get; set; }

        public ProductTypes ProductTypes { get; set; }

        //User
        public int LastUserUpdate { get; set; }
    }
}
