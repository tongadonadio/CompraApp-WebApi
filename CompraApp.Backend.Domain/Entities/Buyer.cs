using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompraApp.Backend.Domain
{
    public class Buyer
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Notifications { get; set; }
        public Buyer() { }

        public double Longitud { get; set; }
        public double Latitud { get; set; }
    }
}
