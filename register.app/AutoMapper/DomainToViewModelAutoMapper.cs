using AutoMapper;
using register.app.ViewModels;
using register.domain.Entities;

namespace register.app.AutoMapper
{
    public class DomainToViewModelAutoMapper : Profile
    {
        public DomainToViewModelAutoMapper()
        {
            CreateMap<Customer, CustomerViewModel>().ReverseMap();
        }
    }
}
