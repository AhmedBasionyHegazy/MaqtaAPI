using FluentValidation;
using Maqta.API.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maqta.API.Validators
{
    public class ProductDtoValidator:AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(p => p.Name).MaximumLength(50).MinimumLength(3).WithMessage("Name Length Must Be Greater Than 2 and Less Than or equal 50");
            RuleFor(p => p.Price).GreaterThan(0).LessThan(1000).WithMessage("Price Must Be Greater Than 0 and Less Than 1000");
            RuleFor(p => p.Description).MaximumLength(200).MinimumLength(5).WithMessage("Description Length Must Be Greater Than 5 and Less Than or equal 200");
        }
    }
}
