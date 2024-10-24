using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Notifications.Application.Abstractions;

namespace ThriveActiveWellness.Modules.Notifications.Application.PARQ.GenerateParqPdf;

public sealed class GenerateParqPdfCommandHandler(IPdfGenerator pdfGenerator) : ICommandHandler<GenerateParqPdfCommand, byte[]>
{
    public Task<Result<byte[]>> Handle(GenerateParqPdfCommand request, CancellationToken cancellationToken)
    {
        byte[] pdfBytes = pdfGenerator.GenerateParqPdf(request.ParQItems);
        
        if (pdfBytes.Length == 0)
        {
            return Task.FromResult(Result.Failure<byte[]>(
                new Error(
                    "Parq.Pdf.Generation.Failed",
                    "Failed to generate PARQ PDF",
                    ErrorType.Failure)));
        }
        
        return Task.FromResult(Result.Success(pdfBytes));
    }
}
