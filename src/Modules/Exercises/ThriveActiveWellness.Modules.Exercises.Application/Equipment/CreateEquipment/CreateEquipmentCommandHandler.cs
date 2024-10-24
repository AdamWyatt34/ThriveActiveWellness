using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Exercises.Application.Abstractions.Data;
using ThriveActiveWellness.Modules.Exercises.Domain.Equipment;

namespace ThriveActiveWellness.Modules.Exercises.Application.Equipment.CreateEquipment;

public class CreateEquipmentCommandHandler(IEquipmentRepository equipmentRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateEquipmentCommand, Guid>
{

    public async Task<Result<Guid>> Handle(CreateEquipmentCommand request, CancellationToken cancellationToken)
    {
        var equipment = Domain.Equipment.Equipment.Create(EquipmentId.New(), request.Name);
        
        equipmentRepository.Add(equipment);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return equipment.Id.Value;
    }
}
