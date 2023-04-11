using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioV7.Modelos
{
    public class UsuarioAplicacion:IdentityUser
    {
        [Required(ErrorMessage ="Nombres es requerido")]
        [MaxLength(80)]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Apellidos es requerido")]
        [MaxLength(80)]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Dirección es requerido")]
        [MaxLength(200)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Ciudad es requerido")]
        [MaxLength(60)]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "País es requerido")]
        [MaxLength(60)]
        public string Pais { get; set; }

        //Referencia que no se agrega a la tabla en la BD
        [NotMapped]
        public string Role { get; set; }
    }
}
