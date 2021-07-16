using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoSalon.ViewModels
{
    public class ProizvodjacViewModel
    {

        public decimal Proizvodjacid { get; set; }
        public string Nazivproizvodjaca { get; set; }

        public ICollection<ModelViewModel> Models { get; set; }
    }
}
