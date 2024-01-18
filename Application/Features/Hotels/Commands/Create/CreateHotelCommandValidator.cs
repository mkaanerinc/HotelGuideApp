using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotels.Commands.Create;

public class CreateHotelCommandValidator : AbstractValidator<CreateHotelCommand>
{
    public CreateHotelCommandValidator()
    {
        RuleFor(c => c.ManagerFirstName).NotEmpty().MinimumLength(2);
        RuleFor(c => c.ManagerLastName).NotEmpty().MinimumLength(2);
        RuleFor(c => c.CompanyName).NotEmpty().MinimumLength(2);
    }
}
