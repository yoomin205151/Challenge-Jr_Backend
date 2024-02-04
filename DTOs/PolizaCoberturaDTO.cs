namespace AdminPolizasAPI.DTOs
{
    public class PolizaCoberturaDTO
    {
        public int Id { get; set; }
        public int PolizaId { get; set; }
        public string Poliza { get; set; }
        public int CoberturaId { get; set; }
        public string Cobertura { get; set; }
        public decimal MontoAsegurado { get; set; }

    }
}
