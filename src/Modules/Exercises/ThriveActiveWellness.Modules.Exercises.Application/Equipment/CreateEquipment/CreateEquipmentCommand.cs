using ThriveActiveWellness.Common.Application.Messaging;

namespace ThriveActiveWellness.Modules.Exercises.Application.Equipment.CreateEquipment;

public sealed record CreateEquipmentCommand(string Name) : ICommand<Guid>;
