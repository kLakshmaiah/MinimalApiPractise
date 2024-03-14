using AutoMapper;
using MinimalApiPractise.DTO;

namespace MinimalApiPractise.Mapper
{
    public class CustomMappingConfig:Profile
    {
        public CustomMappingConfig() 
        { 
          CreateMap<Employee,EmployeeDto>().ReverseMap();//two way binding.
        }
    }
}
