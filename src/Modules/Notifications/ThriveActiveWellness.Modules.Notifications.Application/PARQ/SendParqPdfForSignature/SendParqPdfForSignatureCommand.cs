using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Modules.Notifications.Application.PARQ.GenerateParqPdf;

namespace ThriveActiveWellness.Modules.Notifications.Application.PARQ.SendParqPdfForSignature;

public record SendParqPdfForSignatureCommand(Guid UserId, IEnumerable<ParQItemModel> ParQItems) : ICommand;

// Create command handler after adding in nuget package for BoldSign
