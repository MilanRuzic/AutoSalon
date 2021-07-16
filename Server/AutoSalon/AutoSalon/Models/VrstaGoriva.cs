using System;
using System.Collections.Generic;

#nullable disable

namespace AutoSalon.Models
{
    public partial class VrstaGoriva
    {
      
        public decimal Vrstagorivaid { get; set; }
        public string Nazivvrstegoriva { get; set; }

        public virtual ICollection<Vozilo> Vozilos { get; set; }
    }
}
