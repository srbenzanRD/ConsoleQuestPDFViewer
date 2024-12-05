using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Reflection.Metadata;

namespace QuestPDFViewer.Extentions;

public static class QuestPDFExtention
{
    #region Conduce
    public static void FillConduce(this IDocumentContainer document, ConduceReportDto conduce)
    {
        document.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(1, Unit.Centimetre);
            page.DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Black));

            //Cabecera
            page.HeaderConduce(conduce.Suplidor, "conduce", conduce.Tag.ToString());
            //Contenido
            page.ContentConduce(conduce);
        });
    }
    public static void HeaderConduce(this PageDescriptor page, SuplidorReportDto suplidor, string titulo, string? identificadorTAG)
    {
        page.Header().Container().Column(c =>
        {
            if (identificadorTAG != null)
            {
                c.Item().Text(identificadorTAG).AlignEnd().FontSize(14).ExtraBlack();
            }
            c.Item().Text(suplidor.Nombre).FontSize(18).Bold().AlignCenter();
            if (!string.IsNullOrEmpty(suplidor.Lema)) c.Item().Text(suplidor.Lema).FontSize(10).AlignCenter();
            c.Item().Text(suplidor.Direccion).FontSize(10).AlignCenter();
            c.Item().Text($"Teléfono: {suplidor.Telefono} RNC: {suplidor.RNC}").FontSize(10).AlignCenter();
            c.Item().Text("");
            c.Item().Text(titulo.ToUpper()).FontSize(14).Bold().AlignCenter();
        });
    }
    public static void ContentConduce(this PageDescriptor page, ConduceReportDto conduce)
    {
        var gris = QuestPDF.Infrastructure.Color.FromRGB(211, 211, 211);
        page.Content().Container().Table(table => {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn();//1
                columns.RelativeColumn();//2
                columns.RelativeColumn();//3
                columns.RelativeColumn();//4
                columns.RelativeColumn();//5
                columns.RelativeColumn();//6
                columns.RelativeColumn();//7
            });
            //Contenido
            table.Cell().ColumnSpan(5).AlignRight().Padding(2).Text("NO.: ").ExtraBold().ExtraBlack();
            table.Cell().ColumnSpan(2).BorderBottom(1).Padding(2).AlignCenter().Text(conduce.Secuencia.ToString());
            table.Cell().ColumnSpan(5).AlignRight().Padding(2).Text("FECHA.: ").ExtraBold().ExtraBlack();
            table.Cell().ColumnSpan(2).BorderBottom(1).Padding(2).AlignCenter().Text(conduce.Fecha.ToShortDateString());

            //ROW1 Nombre y Codigo
            table.Cell().ColumnSpan(2).AlignRight().Padding(2).Text("Nombre del centro.: ").Bold();
            table.Cell().ColumnSpan(2).BorderBottom(1).Padding(2).Text(conduce.Centro.Nombre);
            table.Cell().ColumnSpan(2).AlignRight().Padding(2).Text("Código.: ").Bold();
            table.Cell().ColumnSpan(1).BorderBottom(1).Padding(2).Text(conduce.Centro.Codigo);
            //ROW2 Director y Telefono
            table.Cell().ColumnSpan(2).AlignRight().Padding(2).Text("Director del centro.: ").Bold();
            table.Cell().ColumnSpan(2).BorderBottom(1).Padding(2).Text(conduce.Centro.Director);
            table.Cell().ColumnSpan(2).AlignRight().Padding(2).Text("Teléfono.: ").Bold();
            table.Cell().ColumnSpan(1).BorderBottom(1).Padding(2).Text(conduce.Centro.Telefono);
            //ROW3 Direccion
            table.Cell().ColumnSpan(2).AlignRight().Padding(2).Text("Dirección del centro.: ").Bold();
            table.Cell().ColumnSpan(5).BorderBottom(1).Padding(2).Text(conduce.Centro.Direccion);
            //ROW4 Provincia y Distrito
            table.Cell().ColumnSpan(2).AlignRight().Padding(2).Text("Provincia o Municipio.: ").Bold();
            table.Cell().ColumnSpan(2).BorderBottom(1).Padding(2).Text(conduce.Centro.Provincia);
            table.Cell().ColumnSpan(2).AlignRight().Padding(2).Text("Regional/Distrito.: ").Bold();
            table.Cell().ColumnSpan(1).BorderBottom(1).Padding(2).Text(conduce.Centro.Distrito);
            table.Cell().ColumnSpan(7).Text("");
            //Sub-Tabla detalle de entrega
            table.Cell().ColumnSpan(7).Border(1).Background(gris).Padding(5).AlignCenter().Padding(5).Text("DETALLE DE LAS RACIONES ENTREGADAS Y RECIBIDAS").Bold();
            table.Cell().RowSpan(2).ColumnSpan(2).Border(1).Background(gris).Padding(5).AlignMiddle().AlignCenter().Padding(5).Text("RACIONES\r\nALIMENTICIAS\r\nCON POSTRE").Bold();
            table.Cell().ColumnSpan(4).Border(1).Background(gris).Padding(2).AlignCenter().Padding(5).Text("DESCRIPCION DEL PRODUCTO").Bold();
            table.Cell().Border(1).Background(gris).Padding(2).AlignCenter().Padding(2).Text("CANTIDAD").Bold();
            //Row Variables Descripcion y cantidad
            table.Cell().ColumnSpan(4).Border(1).Padding(5).Text(conduce.Servicio).Justify();
            table.Cell().ColumnSpan(1).Border(1).AlignCenter().AlignMiddle().Padding(5).Text(conduce.Raciones.ToString("N0")).AlignCenter();
            table.Cell().ColumnSpan(7).Border(1).AlignRight().Padding(5).Text($"Matrícula total: {conduce.Centro.Estudiantes.ToString("N0")}").FontColor(gris);
            table.Cell().ColumnSpan(7).Text("");
            table.Cell().ColumnSpan(7).BorderBottom(1).Padding(5).AlignLeft().Text("Observaciones:").Bold();
            table.Cell().ColumnSpan(7).BorderBottom(1).Padding(5).AlignLeft().Text("").FontColor(Color.FromRGB(0, 0, 0)).Bold();
            table.Cell().ColumnSpan(7).Text(""); table.Cell().ColumnSpan(7).Text("");
            table.Cell().ColumnSpan(3).Text("");
            table.Cell().ColumnSpan(4).BorderBottom(1).AlignCenter().Padding(1).Text("RECIBIDO POR").Bold();
            table.Cell().ColumnSpan(3).AlignLeft().Padding(1).Text("");
            table.Cell().ColumnSpan(4).BorderBottom(1).AlignLeft().Padding(8).Text("Nombre: ").Bold();
            table.Cell().ColumnSpan(3).AlignLeft().Padding(1).Text("");
            table.Cell().ColumnSpan(4).BorderBottom(1).AlignLeft().Padding(8).Text("Firma: ").Bold();
            table.Cell().ColumnSpan(3).AlignLeft().Padding(1).Text("");
            table.Cell().ColumnSpan(4).BorderBottom(1).AlignLeft().Padding(8).Text("Fecha de recepción:").Bold();
            table.Cell().ColumnSpan(3).AlignLeft().Padding(1).Text("");
            table.Cell().ColumnSpan(4).BorderBottom(1).AlignLeft().Padding(8).Text("Hora de recepción:").Bold();
            table.Cell().ColumnSpan(7).AlignLeft().Padding(1).Text("");
            table.Cell().ColumnSpan(7).AlignLeft().Padding(1).Text("");
            table.Cell().ColumnSpan(7).AlignLeft().Padding(1).Text("");
            table.Cell().ColumnSpan(7).AlignLeft().Padding(1).Text("");
            table.Cell().ColumnSpan(7).AlignLeft().Padding(1).Text("");
            table.Cell().ColumnSpan(3).AlignLeft().Padding(1).AlignCenter().BorderTop(1).Text("FIRMA Y SELLO DEL SUPLIDOR");
            table.Cell().ColumnSpan(1).AlignLeft().Padding(1).Text("");
            table.Cell().ColumnSpan(2).AlignLeft().Padding(1).AlignCenter().BorderTop(1).Text("SELLO DEL CENTRO");
            table.Cell().ColumnSpan(1).AlignLeft().Padding(1).Text("");
        });
    }
    #endregion

    //public static void DefinirCabecera(this PageDescriptor page, SuplidorReportDto suplidor, string titulo, string subtitulo1, string subtitulo2, DateTime fecha)
    //{
    //    page.Header().Container().AlignCenter().Column(c =>
    //    {
    //        c.Item().Text(suplidor.Nombre.ToUpper()).FontSize(18).Bold();
    //        if(!string.IsNullOrEmpty(suplidor.Lema)) c.Item().Text(suplidor.Lema).FontSize(10);
    //        c.Item().Text(suplidor.Direccion).FontSize(10);
    //        c.Item().Text($"Teléfono: {suplidor.Telefono} RNC: {suplidor.RNC}").FontSize(10);
    //        c.Spacing(5, QuestPDF.Infrastructure.Unit.Point);
    //        c.Item().Text(titulo.ToUpper()).FontSize(14).Bold();
    //        c.Spacing(5, QuestPDF.Infrastructure.Unit.Point);
    //        if (!string.IsNullOrEmpty(subtitulo1)) c.Item().Text(subtitulo1).FontSize(12).Bold();
    //        if (!string.IsNullOrEmpty(subtitulo2)) c.Item().Text(subtitulo2).FontSize(12).Bold(); 
    //        c.Item().Text($"Fecha: {fecha.ToShortDateString()}").AlignEnd().FontSize(12).Bold();
    //    });
    //}


}