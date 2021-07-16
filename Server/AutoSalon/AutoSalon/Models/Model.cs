using System;
using System.Collections.Generic;

#nullable disable

namespace AutoSalon.Models
{
    public partial class Model
    {
        //public Model()
        //{
        //    Vozilos = new HashSet<Vozilo>();
        //}

        public decimal Proizvodjacid { get; set; }
        public decimal Modelid { get; set; }
        public string Nazivmodela { get; set; }

        public virtual Proizvodjac Proizvodjac { get; set; }
        public virtual ICollection<Vozilo> Vozilos { get; set; }
    }
}
