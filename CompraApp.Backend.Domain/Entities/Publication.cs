using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompraApp.Backend.Domain
{
    public class Publication
    {
        public enum STATE { OPEN = 0, CLOSE_BY_ACEPTED_OFFER = 1, CLOSE_BY_USER = 2 };

        public int Id { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int IdBuyer { get; set; }
        [Required]
        public int State { get; set; }
        public int DeliveryItem { get; set; }
        public string DescriptionItem { get; set; }
        public int PriceMinItem { get; set; }
        public int PriceMaxItem { get; set; }
        public int StateItem { get; set; }

        public int Urgency { get; set; }
        public int Type { get; set; }
        [NotMapped]
        public string NameBuyer { get; set; }

        public Buyer Buyer { get; set; }
        public Publication() { }

        [NotMapped]
        public int CountOffers{get; set;}
    }
}
