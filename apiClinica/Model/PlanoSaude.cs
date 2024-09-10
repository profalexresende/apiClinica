// Models/PlanoSaude.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiClinica.Model
{
    public class PlanoSaude
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // ID auto incremento

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(50)]
        public string Cobertura { get; set; }
    }
}
