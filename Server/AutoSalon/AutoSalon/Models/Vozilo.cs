using System;
using System.Collections.Generic;

#nullable disable

namespace AutoSalon.Models
{
    public partial class Vozilo
    {
        public Vozilo()
        {
            Slikes = new HashSet<Slike>();
        }

        public decimal Voziloid { get; set; }
        public decimal Vrstagorivaid { get; set; }
        public decimal? Kupacid { get; set; }
        public decimal Proizvodjacid { get; set; }
        public decimal Modelid { get; set; }
        public decimal Kilometraza { get; set; }
        public int Snagamotora { get; set; }
        public string Opis { get; set; }
        public decimal Cena { get; set; }
        public DateTime Datumdolaska { get; set; }
        public DateTime? Datumprodaje { get; set; }

        public virtual Kupac Kupac { get; set; }
        public virtual Model Model { get; set; }
        public virtual VrstaGoriva Vrstagoriva { get; set; }
        public virtual ICollection<Slike> Slikes { get; set; }
    }
}
