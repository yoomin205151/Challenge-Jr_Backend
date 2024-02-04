using System.ComponentModel.DataAnnotations;

namespace AdminPolizasAPI.Entidades
{
    public class Cobertura
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Nombre { get; set; }

        public bool? ResponsabilidadCivil { get; set; }

        public bool? DestruccionTotalAccidentes { get; set; }

        public bool? CristalesLaterales { get; set; }

        public bool? LunetasParabrisas { get; set; }

        public bool? Cerraduras { get; set; }
    }
}
