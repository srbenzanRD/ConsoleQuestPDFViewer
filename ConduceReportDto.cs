namespace QuestPDFViewer;
public record ConduceReportDto(
    int IdConduce,
    int IdContratoDetalle,
    int Secuencia,
    int Tag,
    DateTime Fecha,
    SuplidorReportDto Suplidor,
    CentroReportDto Centro,
    ContratoConfiguracionReportDto Contrato,
    string Servicio,
    int Raciones)
{
    public decimal Valor => Raciones * Contrato.CostoPlato;
};
public record CentroReportDto(
    string Codigo, 
    string Nombre, 
    string Director, 
    string Telefono, 
    string Direccion, 
    string Provincia, 
    string Distrito,
    int Estudiantes);
public record SuplidorReportDto(string Nombre, string RNC, string Direccion, string Lema, string Telefono);
public record ContratoConfiguracionReportDto(string TipoContrato, string CodigoContrato, decimal CostoPlato);


public record FacturaReportDto(List<ConduceReportDto> conduces, DateTime FechaAutorizacionDGII, int secuenciaB15, bool nueva = false)
{
    public DateTime Fecha => 
        ultimoConduce != null ? 
        ultimoConduce!.Fecha : DateTime.Now;

    private ConduceReportDto? primerConduce => 
        conduces.Any() ? 
        conduces.MinBy(x => x.Secuencia) : null;

    private ConduceReportDto? ultimoConduce => 
        conduces.Any() ? 
        conduces.MaxBy(x => x.Secuencia) : null;

    public string Periodo => "N/A";
    public string NCF => $"B15{(secuenciaB15)}";
    public string CantidadConduces => $"{conduces.Count} del No. {(primerConduce!=null?primerConduce.Secuencia:"#")} al {(ultimoConduce!=null? ultimoConduce.Secuencia:"#")}.";
    public int Raciones => conduces.Any() ? conduces.Sum(c => c.Raciones) : 0;
    public decimal Monto => conduces.Any() ? conduces.Sum(c => c.Valor) : 0;
    public ContratoConfiguracionReportDto? Contrato => conduces.Any() ? conduces.FirstOrDefault()!.Contrato: null;
    public string Concepto => $"Concepto..............";
}