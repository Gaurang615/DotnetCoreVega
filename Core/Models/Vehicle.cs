using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vega.Core.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        public int Id { get; set; } 
        public int ModelId { get; set; }    
        public Model Model { get; set; }
        public bool IsRegistered { get; set; }  
        
        [Required]
        [StringLength(255)]
        public string ContactName { get; set; }
        
        [Required]
        public string ContactEmail { get; set; }

        [Required]
        [StringLength(255)]
        public string ContactPhone { get; set; }    

        public DateTime LastUpdatedWhen { get; set; }
        public string LastUpdatedByUser { get; set; }

        public ICollection<VehicleFeature> Features { get; set; }

        public Vehicle()
        {
            Features=new Collection<VehicleFeature>();
        }
    }
}