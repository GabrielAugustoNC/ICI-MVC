using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ProvaCandidato.Data.Entidade
{
    [Table("Cidade")]
    public class Cidade
    {
        [Key]
        [Column("codigo")]
        [DisplayName("Código")]
        public int Codigo { get; set; }

        [Column("nome")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome da cidade deve ter entre 3 e 50 caracteres")]
        [DisplayName("Cidade")]
        [Required]
        public string Nome { get; set; }
    }
}
