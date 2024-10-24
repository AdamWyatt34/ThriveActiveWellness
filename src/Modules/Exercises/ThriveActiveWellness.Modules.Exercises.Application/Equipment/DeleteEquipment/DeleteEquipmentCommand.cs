using ThriveActiveWellness.Common.Application.Messaging;

namespace ThriveActiveWellness.Modules.Exercises.Application.Equipment.DeleteEquipment;

public sealed record DeleteEquipmentCommand(Guid EquipmentId) : ICommand;
