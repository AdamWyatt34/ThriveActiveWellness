using FluentValidation;

namespace ThriveActiveWellness.Modules.Exercises.Application.Equipment.CreateEquipment;

public class CreateEquipmentCommandValidator : AbstractValidator<CreateEquipmentCommand>
{
    public CreateEquipmentCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(255);
    }
}

