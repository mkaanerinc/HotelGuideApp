using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ContactInformations.Commands.Create;

public class CreateContactInformationCommandValidator : AbstractValidator<CreateContactInformationCommand>
{
    public CreateContactInformationCommandValidator()
    {
        RuleFor(c => c.HotelId).NotEmpty();
        RuleFor(c => c.InfoType).NotEmpty();
        RuleFor(c => c.InfoContent).NotEmpty().MinimumLength(2);
    }
}
