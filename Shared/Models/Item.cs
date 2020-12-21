using GridShared.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace JINIApp.Shared.Models
{    
    public partial class Item
    {
        [Key]
        [GridHiddenColumn]
        [Required]
        [GridColumn(Position = 0)]
        public int ID { get; set; }

        
        [GridColumn(Position = 1, Title = "Supplier", Width = "120px", SortEnabled = true, FilterEnabled = true)]
        [Required]
        public string Supplier { get; set; }

        [GridColumn(Position = 2, Title = "Address", Width = "120px", SortEnabled = true, FilterEnabled = true)]
        public string Address { get; set; }

        [GridColumn(Position = 3, Title = "ContactNumber", Width = "120px", SortEnabled = true, FilterEnabled = true)]                
        public string ContactNumber { get; set; }

        [GridColumn(Position = 4, Title = "ItemName", Width = "120px", SortEnabled = true, FilterEnabled = true)]
        [Required]
        public string ItemName { get; set; }
        


    }
}
