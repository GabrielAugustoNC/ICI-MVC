using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ProvaCandidato.Data.Entidade
{
    [Table("ClienteObservacao")]
    public class ClienteObservacao
    {
        [Key]
        [Column("codigo")]
        public int Codigo { get; set; }

        [Column("observacao")]
        [DisplayName("Observação")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "A observação deve ter entre 3 e 50 caracteres")]
        [Required]
        public string Observacao { get; set; }

        [Column("referencia")]
        [DisplayName("Referência")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "A referencia deve ter entre 3 e 50 caracteres")]
        [Required]
        public string Referencia { get; set; }

        public Cliente Cliente { get; set; }
    }
}
