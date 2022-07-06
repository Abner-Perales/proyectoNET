using GymManager.Core.Inventories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.Core.Sales
{
    public class SalesInventory
    {
        public int SalesId { get; set; }

        public int InventoryId { get; set; }

        public Sales Sales { get; set; }

        public Inventory Inventory { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Enter valid quantity.")]
        public int Quantity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Enter valid cost.")]
        [DataType(DataType.Currency)]
        public double SalesCost { get; set; }
    }
}
