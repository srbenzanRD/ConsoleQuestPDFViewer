using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDFViewer;
using QuestPDFViewer.Extentions;
// Licencia para QuestPDF
QuestPDF.Settings.License = LicenseType.Community;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var conduce = new ConduceReportDto(1, 1, 1, 1, DateTime.Now, 
new SuplidorReportDto("Suplidor", "1234567890", "Direccion", "Lema", "1234567890"), 
new CentroReportDto("1234567890", "Centro", "Director", "1234567890", "Direccion", "Provincia", "Distrito", 100), 
new ContratoConfiguracionReportDto("Tipo", "Contrato", 100), "Este es el servicio que se llevará al centro Educativo, correspondiente a ese dia. Este es el servicio que se llevará al centro Educativo, correspondiente a ese dia. Este es el servicio que se llevará al centro Educativo, correspondiente a ese dia. Este es el servicio que se llevará al centro Educativo, correspondiente a ese dia.", 100);

 var doc = Document.Create(document =>
        {
            document.FillConduce(conduce);
        });
doc.ShowInCompanion(12500);