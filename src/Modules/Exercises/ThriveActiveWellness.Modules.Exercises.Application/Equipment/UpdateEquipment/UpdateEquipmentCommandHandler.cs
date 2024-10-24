using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Exercises.Application.Abstractions.Data;
using ThriveActiveWellness.Modules.Exercises.Domain.Equipment;

namespace ThriveActiveWellness.Modules.Exercises.Application.Equipment.UpdateEquipment;

public class UpdateEquipmentCommandHandler(IEquipmentRepository equipmentRepository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateEquipmentCommand>
{
    public async Task<Result> Handle(UpdateEquipmentCommand request, CancellationToken cancellationToken)
    {
        Domain.Equipment.Equipment? equipment = await equipmentRepository.GetByIdAsync(new EquipmentId(request.EquipmentId));
        
        if (equipment is null)
        {
            return Result.Failure(EquipmentErrors.NotFound);
        }
        
        equipment.Update(request.Name);
        
        equipmentRepository.Update(equipment);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}
