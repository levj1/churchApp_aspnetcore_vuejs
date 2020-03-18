using AutoMapper;
using ChurchAppAPI.Entities;
using ChurchAppAPI.Models;

namespace ChurchAppAPI.Extensions.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, AppUserDTo>();
            CreateMap<AppUserDTo, AppUser>();

            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto, Person>();

            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, Address>();

            CreateMap<AddressType, AddressTypeDto>();
            CreateMap<AddressTypeDto, AddressType>();

            CreateMap<DonationTypeDto, DonationType>();
            CreateMap<DonationType, DonationTypeDto>();

        }
    }
}
