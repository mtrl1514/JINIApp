using GridShared.DataAnnotations;
using GridShared.Sorting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JINIApp.Shared.Models
{
    public class SalesOrder
    {
        public SalesOrder()
        {
            this.SalesOrderItems = new HashSet<SalesOrderItem>();
            this.Revenues = new HashSet<Revenue>();
        }


        [Key]
        [GridHiddenColumn]
        [Required]
        [GridColumn(Position = 0)]
        public int ID { get; set; }        

        [GridColumn(Position = 1, Title = "SalesOrderNo", Width = "120px", SortEnabled = true, FilterEnabled = true)]
        [Required]        
        public string SalesOrderNo { get; set; }

        [GridColumn(Position = 2, Title = "SalesDate", Width = "120px", Format = "{0:yyyy-MM-dd}", SortEnabled = true, FilterEnabled = true, SortInitialDirection = GridSortDirection.Ascending)]
        [DataType(DataType.Date)]
        [Required]
        public DateTime? SalesDate { get; set; }

        [GridColumn(Position = 3)]
        [Required]
        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        [GridColumn(Position = 4)]
        [NotMappedColumn]
        public virtual Customer Customer { get; set; }        

        
        [NotMappedColumn]   
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<SalesOrderItem> SalesOrderItems { get; set; }

        
        [NotMappedColumn]
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Revenue> Revenues { get; set; }

    }
}
