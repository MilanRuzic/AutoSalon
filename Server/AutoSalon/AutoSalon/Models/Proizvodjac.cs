using System;
using System.Collections.Generic;

#nullable disable

namespace AutoSalon.Models
{
    public partial class Proizvodjac
    {

        public decimal Proizvodjacid { get; set; }
        public string Nazivproizvodjaca { get; set; }

        public virtual ICollection<Model> Models { get; set; }
    }
}
