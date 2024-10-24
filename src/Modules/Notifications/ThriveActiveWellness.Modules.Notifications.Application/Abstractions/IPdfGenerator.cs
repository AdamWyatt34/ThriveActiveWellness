using ThriveActiveWellness.Modules.Notifications.Application.PARQ.GenerateParqPdf;

namespace ThriveActiveWellness.Modules.Notifications.Application.Abstractions;

public interface IPdfGenerator
{
    byte[] GenerateParqPdf(IEnumerable<ParQItemModel> parQItems);
}
