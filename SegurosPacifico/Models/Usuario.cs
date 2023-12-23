using NuGet.Protocol.Core.Types;
using System.ComponentModel.DataAnnotations;

namespace SegurosPacifico.Models
{
    public class Usuario
    {
        [Key]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Nombre { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
