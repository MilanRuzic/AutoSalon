using AutoMapper;
using AutoSalon.Models;
using AutoSalon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoSalon.Profiles
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Vozilo, VoziloViewModel>().ReverseMap();
            CreateMap<Kupac, KupacViewModel>().ReverseMap();
            CreateMap<Slike, SlikeViewModel>().ReverseMap();
            CreateMap<VrstaGoriva, VrstaGorivaViewModel>().ReverseMap();
            CreateMap<Model, ModelViewModel > ().ReverseMap();
            CreateMap<Proizvodjac, ProizvodjacViewModel>().ReverseMap();

        }
    }
}
