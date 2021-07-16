using AutoSalon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoSalon.ViewModels
{
    public class ModelViewModel
    {

       
        public decimal Modelid { get; set; }
        public string Nazivmodela { get; set; }
        public virtual ProizvodjacViewModel Proizvodjac { get; set; }


    }
}
