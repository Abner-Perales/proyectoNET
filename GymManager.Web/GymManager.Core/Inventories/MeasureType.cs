using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.Core.Inventories
{
    public class MeasureType
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        public List<Inventory> Inventories { get; set; }

        public MeasureType()
        {
            Inventories = new List<Inventory>();
        }
    }
}
