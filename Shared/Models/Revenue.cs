using GridShared.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JINIApp.Shared.Models
{
    public class Revenue
    {
        [Key]
        [GridHiddenColumn]
        [Required]
        [GridColumn(Position = 0)]
        public int ID { get; set; }

        [GridColumn(Position = 1, Title = "결제일자", Width = "120px", SortEnabled = true, FilterEnabled = true)]
        public DateTime? RevenueDate { get; set; }

        [GridColumn(Position = 2, Title = "결제일자", Width = "120px", SortEnabled = true, FilterEnabled = true)]
        public decimal RevenueCost { get; set; }

        [GridColumn(Position = 3)]
        [Required]
        public int SalesOrderID { get; set; }


        [ForeignKey("SalesOrderID")]
        [NotMappedColumn]
        public virtual SalesOrder SalesOrder { get; set; }
    }
}
