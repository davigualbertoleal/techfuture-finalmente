using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechFutureApi.Data
{
    // --- CONTEXTO DO BANCO ---
    public class TechFutureContext : DbContext
    {
        public TechFutureContext(DbContextOptions<TechFutureContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Estoque> Estoque { get; set; }
        public DbSet<Maquina> Maquinas { get; set; }
        public DbSet<MovimentoEstoque> MovimentoEstoque { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<CompraFornecedor> ComprasFornecedores { get; set; }
        public DbSet<ItemCompra> ItensCompra { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações de Enum e Chaves se necessário
            modelBuilder.Entity<Estoque>().HasKey(e => e.IdProduto);
            modelBuilder.Entity<PessoaFisica>().HasKey(p => p.IdUsuario);
            modelBuilder.Entity<PessoaJuridica>().HasKey(p => p.IdUsuario);
        }
    }

    // --- TABELAS ---

    [Table("produtos")]
    public class Produto
    {
        [Column("id")] public int Id { get; set; }
        [Column("nomeProduto")] public string Nome { get; set; }
        [Column("tipo")] public string Tipo { get; set; } // 'peca' ou 'pcMontado'
        [Column("preco")] public decimal Preco { get; set; }
    }

    [Table("estoque")]
    public class Estoque
    {
        [Column("idProduto")] public int IdProduto { get; set; }
        [ForeignKey("IdProduto")] public Produto Produto { get; set; }
        [Column("quantidadeAtual")] public int QtdAtual { get; set; }
        [Column("quantidadeMinima")] public int QtdMinima { get; set; }
    }

    [Table("maquinas")]
    public class Maquina
    {
        [Column("id")] public int Id { get; set; }
        [Column("categoria")] public string Categoria { get; set; } // 'fraca','media','potente'
        [Column("status")] public string Status { get; set; } // 'estoque','vendida'
        [Column("idProduto")] public int IdProduto { get; set; }
        [ForeignKey("IdProduto")] public Produto Produto { get; set; }
    }

    [Table("movimentoestoque")]
    public class MovimentoEstoque
    {
        [Column("id")] public int Id { get; set; }
        [Column("tipo")] public string Tipo { get; set; } // 'entrada','saida'
        [Column("quantidade")] public int Quantidade { get; set; }
        [Column("dataMovimento")] public DateTime Data { get; set; }
    }

    [Table("usuario")]
    public class Usuario
    {
        [Column("id")] public int Id { get; set; }
        [Column("tipo")] public string Tipo { get; set; } // 'consumidor','empresa'
    }

    [Table("pessoafisica")]
    public class PessoaFisica
    {
        [Column("idUsuario")] public int IdUsuario { get; set; }
    }
    
    [Table("pessoajuridica")]
    public class PessoaJuridica
    {
        [Column("idUsuario")] public int IdUsuario { get; set; }
    }

    [Table("pedidos")]
    public class Pedido
    {
        [Column("id")] public int Id { get; set; }
        [Column("idUsuario")] public int IdUsuario { get; set; }
        [Column("tipoVenda")] public string TipoVenda { get; set; } // 'online','presencial'
        [Column("dataPedido")] public DateTime Data { get; set; }
    }

    [Table("itenspedido")]
    public class ItemPedido
    {
        [Column("id")] public int Id { get; set; }
        [Column("idPedido")] public int IdPedido { get; set; }
        [Column("idProduto")] public int IdProduto { get; set; }
        [ForeignKey("IdProduto")] public Produto Produto { get; set; }
        [Column("quantidade")] public int Quantidade { get; set; }
    }

    [Table("fornecedores")]
    public class Fornecedor
    {
        [Column("id")] public int Id { get; set; }
        [Column("nome")] public string Nome { get; set; }
    }

    [Table("comprasfornecedores")]
    public class CompraFornecedor
    {
        [Column("id")] public int Id { get; set; }
        [Column("idFornecedor")] public int IdFornecedor { get; set; }
        [ForeignKey("IdFornecedor")] public Fornecedor Fornecedor { get; set; }
    }

    [Table("itenscompra")]
    public class ItemCompra
    {
        [Column("id")] public int Id { get; set; }
        [Column("idCompra")] public int IdCompra { get; set; }
        [Column("idProduto")] public int IdProduto { get; set; }
        [ForeignKey("IdProduto")] public Produto Produto { get; set; }
        [Column("quantidade")] public int Quantidade { get; set; }
    }
}