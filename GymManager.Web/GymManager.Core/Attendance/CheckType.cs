using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.Core.Attendance
{
    public class CheckType
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Debe ingresar el tipo de checada.")]
        public string Name { get; set; }
        /*
        public List<Check> Checks { get; set; }

        public CheckType()
        {
            Checks = new List<Check>();
        }
        */
    }
}
