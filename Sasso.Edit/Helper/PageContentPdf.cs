using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;


namespace Engine.Edit.Helper
{
    public class PageContentPdf : IDocument
    {
        private readonly string _content;

        public PageContentPdf(string content)
        {
            _content = content ?? string.Empty;
        }

        public DocumentMetadata GetMetadata()
            => DocumentMetadata.Default;

        public DocumentSettings GetSettings()
            => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);

                page.Content().Column(column =>
                {
                    var paragraphs = _content
                        .Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var paragraph in paragraphs)
                    {
                        column.Item().Text(paragraph)
                            .FontSize(12)
                            .LineHeight(1.4f);

                        column.Item().Height(12);
                    }
                });
            });
        }
    }
}
