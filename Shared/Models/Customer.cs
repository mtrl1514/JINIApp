using GridShared.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JINIApp.Shared.Models
{
    public class Customer
    {
        [Key]
        [GridHiddenColumn]
        [Required]
        [GridColumn(Position = 0)]
        public int ID { get; set; }

        [GridColumn(Position = 1, Title = "Name", Width = "120px", SortEnabled = true, FilterEnabled = true)]
        [Required]
        public string Name { get; set; }

        [GridColumn(Position = 2, Title = "Owner", Width = "120px", SortEnabled = true, FilterEnabled = true)]
        public string Owner { get; set; }
        
    }
}
