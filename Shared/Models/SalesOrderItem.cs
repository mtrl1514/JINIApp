using GridShared.DataAnnotations;
using GridShared.Sorting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JINIApp.Shared.Models
{
    public class SalesOrderItem
    {
        [Key]
        [GridHiddenColumn]
        [Required]
        [GridColumn(Position = 0)]
        public int ID { get; set; }

        [GridColumn(Position = 1, Title = "SalesDate", Width = "120px", Format = "{0:yyyy-MM-dd}", SortEnabled = true, FilterEnabled = true, SortInitialDirection = GridSortDirection.Ascending)]
        [Required]
        public DateTime? SalesDate { get; set; }

        public decimal UnitCost { get; set; }

        public decimal UnitProfit { get; set; }

        public decimal SalesQuantity { get; set; }

        public string ItemNumber { get; set; }

        public string ItemColor { get; set; }

        public string Comment { get; set; }

        [GridColumn(Position = 4)]
        [Required]
        public int SalesOrderID { get; set; }

        [ForeignKey("SalesOrderID")]
        [NotMappedColumn]
        public virtual SalesOrder SalesOrder { get; set; }

        [GridColumn(Position = 5)]
        [Required]
        public int ItemID { get; set; }

        [ForeignKey("ItemID")]
        [NotMappedColumn]
        public virtual Item Item { get; set; }


    }
}
