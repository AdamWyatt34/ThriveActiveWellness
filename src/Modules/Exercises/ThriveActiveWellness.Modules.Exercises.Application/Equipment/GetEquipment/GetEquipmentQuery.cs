using ThriveActiveWellness.Common.Application.Messaging;

namespace ThriveActiveWellness.Modules.Exercises.Application.Equipment.GetEquipment;

public sealed record GetEquipmentQuery(Guid EquipmentId) : IQuery<EquipmentResponse>;
