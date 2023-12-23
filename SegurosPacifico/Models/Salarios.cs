namespace SegurosPacifico.Models
{
    public class Salarios
    {
        public int ID { get; set; }

        public string Nombre { get; set; }

        public int HorasNormales { get; set; }

        public int HorasExtra { get; set; }

        public decimal SalarioBruto { get; set; }

        public string Deducciones { get; set; }

        public decimal SalarioNeto{ get; set; }
    }
}
