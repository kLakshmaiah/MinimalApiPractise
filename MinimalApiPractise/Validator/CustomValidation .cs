using FluentValidation;
using MinimalApiPractise.DTO;

namespace MinimalApiPractise.Validator
{
    public class CustomValidation : AbstractValidator<EmployeeDto>
    {
        public CustomValidation()
        {
            //for EmployeeName
            RuleFor(employeeDto => employeeDto.EmployeeName).NotEmpty().WithName("EmployeeName is Not Empty");
            RuleFor(employeeDto => employeeDto.EmployeeName).MinimumLength(3).WithMessage("EmployeeName Muste be greater than 3 Letters");
            
            
        }
    }
}
