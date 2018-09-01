using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompraApp.Backend.Domain.Entities
{
    public class Offer
    {
        public enum ACTION {OPEN=0, REJECTED=1, ACCEPTED=2, CANCEL=3};
        
        public int Id { get; set; }
        [Required]
        public int IdSeller { get; set; }
        [Required]
        public int IdPublication { get; set; }
        [Required]
        public int State { get; set; }
        [Required]
        public int PriceItem { get; set; }
        public int DeliveryItem { get; set; }
        [Required]
        public string DescriptionItem { get; set; }
        [Required]
        public int StateItem { get; set; }        
        public string DeliveryZoneItem { get; set; }

        public string PhotoItem { get; set; }

        public Publication Publication { get; set; }
        public Seller Seller { get; set; }
        public Offer() { }
    }
}
