using AutoMapper;
using FurniMove.DTOs;
using FurniMove.Models;

namespace FurniMove.Profiles
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<MoveRequestWriteDTO, MoveRequest>();
            CreateMap<MoveOfferWriteDTO, MoveOffer>();
            CreateMap<AppUser, UserDTO>();
            CreateMap<LocationWriteDTO, Location>();
            CreateMap<TruckWriteDTO, Truck>();
        }
    }
}
