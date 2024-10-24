using FluentValidation;

namespace ThriveActiveWellness.Modules.Exercises.Application.Equipment.UpdateEquipment;


public class UpdateEquipmentCommandValidator : AbstractValidator<UpdateEquipmentCommand>
{
    public UpdateEquipmentCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(255);
    }
}
