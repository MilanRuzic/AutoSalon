using System;
using System.Collections.Generic;

#nullable disable

namespace AutoSalon.Models
{
    public partial class Slike
    {
        public decimal Voziloid { get; set; }
        public decimal Slikeid { get; set; }
        public string Nazivslike { get; set; }

        public virtual Vozilo Vozilo { get; set; }
    }
}
