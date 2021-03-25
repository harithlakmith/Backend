using AutoMapper;
using TranspotationTicketBooking.Models;

namespace TranspotationTicketBooking
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AdminRegistrationModel, User>()
               .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));

            CreateMap<PassengerRegistration, User>()
               .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));


            CreateMap<BusRegistration, User>()
               .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));

            CreateMap<PassengerRegistration, Passenger>();


            CreateMap<BusRegistration, BusInfo>();

           // CreateMap<PassengerUpdate, User>();
              //.ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));

            CreateMap<PassengerUpdate, Passenger>();


        }
    }
}
