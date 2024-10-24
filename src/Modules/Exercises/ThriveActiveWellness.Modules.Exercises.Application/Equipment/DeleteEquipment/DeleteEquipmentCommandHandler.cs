using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Exercises.Application.Abstractions.Data;
using ThriveActiveWellness.Modules.Exercises.Domain.Equipment;

namespace ThriveActiveWellness.Modules.Exercises.Application.Equipment.DeleteEquipment;

public class DeleteEquipmentCommandHandler(IEquipmentRepository equipmentRepository, IUnitOfWork unitOfWork) : ICommandHandler<DeleteEquipmentCommand>
{
    public async Task<Result> Handle(DeleteEquipmentCommand request, CancellationToken cancellationToken)
    {
        Domain.Equipment.Equipment? equipment = await equipmentRepository.GetByIdAsync(
            new EquipmentId(request.EquipmentId));
        
        if (equipment is null)
        {
            return Result.Failure(EquipmentErrors.NotFound);
        }
        
        // Check if equipment is in use
        
        equipment.Delete();
        equipmentRepository.Update(equipment);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}
