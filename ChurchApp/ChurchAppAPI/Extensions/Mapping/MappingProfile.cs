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
            CreateMap<Person, PersonWithoutAddressDto>();
            CreateMap<PersonWithoutAddressDto, Person>();
            CreateMap<CreatePersonDto, Person>();
            CreateMap<Person, CreatePersonDto>();
            CreateMap<CreatePersonDto, PersonWithoutAddressDto>();
            CreateMap<PersonWithoutAddressDto, CreatePersonDto>();

            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, Address>();
            CreateMap<AddressWithoutAddressTypeDto, Address>();
            CreateMap<Address, AddressWithoutAddressTypeDto>();
            CreateMap<AddressCreateDto, Address>();
            CreateMap<Address, AddressCreateDto>();

            CreateMap<AddressType, AddressTypeDto>();
            CreateMap<AddressTypeDto, AddressType>();
            CreateMap<AddressTypeWithoutIdDto, AddressType>();


            CreateMap<DonationTypeDto, DonationType>();
            CreateMap<DonationType, DonationTypeDto>();
            CreateMap<DonationTypeCreateDto, DonationType>();

            CreateMap<Donation, DonationDto>();
            CreateMap<DonationDto, Donation>();
            CreateMap<Donation, DonationCreateDto>();
            CreateMap<DonationCreateDto, Donation>();
            CreateMap<DonationToUpdate, Donation>();
            CreateMap<Donation, DonationToUpdate>();
            CreateMap<Donation, DonationWithoutPersonDto>();
            CreateMap<DonationWithoutPersonDto, Donation>();
            

        }
    }
}
