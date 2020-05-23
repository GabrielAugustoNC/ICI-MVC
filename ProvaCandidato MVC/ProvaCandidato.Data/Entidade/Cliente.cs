using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvaCandidato.Data.Entidade
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        [Column("codigo")]
        [DisplayName("Código")]
        public int Codigo { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome do cliente deve ter entre 3 e 50 caracteres")]
        [Required]
        [Column("nome")]
        public string Nome { get; set; }

        [Column("data_nascimento")]
        [DisplayName("Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? DataNascimento { get; set; }

        [Column("codigo_cidade")]
        [Display(Name = "Cidade")]
        public int CidadeId { get; set; }

        public bool Ativo { get; set; }

        [ForeignKey("CidadeId")]
        public virtual Cidade Cidade { get; set; }

        [InverseProperty("Cliente")]
        public ICollection<ClienteObservacao> Observacoes { get; set; }

    }
}