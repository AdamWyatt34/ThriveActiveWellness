using ThriveActiveWellness.Common.Application.Messaging;

namespace ThriveActiveWellness.Modules.Exercises.Application.Equipment.UpdateEquipment;

public sealed record UpdateEquipmentCommand(Guid EquipmentId, string Name) : ICommand;
