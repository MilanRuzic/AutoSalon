using AutoSalon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoSalon.ViewModels
{
    public class VoziloViewModel
    {

        public decimal Voziloid { get; set; }

        public decimal Kilometraza { get; set; }
        public int Snagamotora { get; set; }
        public string Opis { get; set; }
        public decimal Cena { get; set; }
        public DateTime Datumdolaska { get; set; }
        public DateTime? Datumprodaje { get; set; }


        public  KupacViewModel Kupac { get; set; }
        public  ModelViewModel Model { get; set; }
        public  VrstaGorivaViewModel Vrstagoriva { get; set; }
        public  ICollection<SlikeViewModel> Slikes { get; set; }
    }
}
