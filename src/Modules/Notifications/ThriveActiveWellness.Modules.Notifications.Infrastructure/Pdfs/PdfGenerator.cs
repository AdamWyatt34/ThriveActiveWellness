using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ThriveActiveWellness.Modules.Notifications.Application.Abstractions;
using ThriveActiveWellness.Modules.Notifications.Application.PARQ.GenerateParqPdf;

namespace ThriveActiveWellness.Modules.Notifications.Infrastructure.Pdfs;

public class PdfGenerator : IPdfGenerator
{
    public byte[] GenerateParqPdf(IEnumerable<ParQItemModel> parQItems)
    {
        var pdfDocument = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.Header().Text("Physical Activity Readiness Questionnaire (PAR-Q)").FontSize(20).Bold();
                page.Content().Element(elementContainer =>
                {
                    BuildQuestionsTable(elementContainer, parQItems);
                });
            });
        });

        return pdfDocument.GeneratePdf();
    }
    
    private static void BuildQuestionsTable(IContainer container, IEnumerable<ParQItemModel> responses)
    {
        container.Table(table =>
        {
            // Define columns
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(40); // For question number
                columns.RelativeColumn();   // For question text
                columns.ConstantColumn(100); // For answer
            });

            // Header
            table.Header(header =>
            {
                header.Cell().Text("#").Bold();
                header.Cell().Text("Question").Bold();
                header.Cell().Text("Answer").Bold();
            });

            // Add rows for each response
            int questionNumber = 1;
            foreach (ParQItemModel response in responses)
            {
                table.Cell().Text(questionNumber.ToString(NumberFormatInfo.InvariantInfo));
                table.Cell().Text(response.Question);
                table.Cell().Text(response.Answer);
                questionNumber++;
            }
        });
    }
}
