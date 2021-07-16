using System;
using System.Collections.Generic;

#nullable disable

namespace AutoSalon.Models
{
    public partial class Kupac
    {
  
        public decimal Kupacid { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }

        public virtual ICollection<Vozilo> Vozilos { get; set; }
    }
}
