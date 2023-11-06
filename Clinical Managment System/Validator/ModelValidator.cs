
using Clinical_Managment_System.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Clinical_Managment_System.Validator
{
    public  class ModelValidator:AbstractValidator<PersonModel>
    {
        private Regex name=new Regex("@\"^[a-zA-Z]+$\"");
        public ModelValidator()
        {
            RuleFor(x => x.FirstName)
                                     .NotEmpty().WithMessage("First Name is Empty")
                                     .Length(2, 50).WithMessage("Length is Invalid")
                                     .Matches(name).WithMessage("name should be in proper format");

            RuleFor(x => x.DateOfBirth).NotNull().NotEmpty().WithMessage("can't be empty");
        }
    }
}
