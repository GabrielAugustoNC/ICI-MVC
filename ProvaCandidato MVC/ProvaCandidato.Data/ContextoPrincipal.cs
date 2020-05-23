using ProvaCandidato.Data.Entidade;
using System.Data.Entity;

namespace ProvaCandidato.Data
{
    public class ContextoPrincipal : DbContext
    {
        //const string CONNECTION_STRING = @"Server=localhost\SQLEXPRESS01;Database=provacandidato;Trusted_Connection=True;";
        const string CONNECTION_STRING = @"data source=localhost;initial catalog=provacandidato;Trusted_Connection=True;";

        public ContextoPrincipal() : base(CONNECTION_STRING) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<ClienteObservacao> ClienteObservacoes { get; set; }
    }
}
