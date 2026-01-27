using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechFutureApi.Data;

namespace TechFutureApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly TechFutureContext _context;

        public DashboardController(TechFutureContext context)
        {
            _context = context;
        }

        // ==========================================
        // FÁBRICA: Produção, Máquinas, Estoque
        // ==========================================
        [HttpGet("fabrica")]
        public IActionResult GetFabrica()
        {
            // 1. O que mais fabrica (Baseado em Maquinas em estoque)
            // Agrupa maquinas pelo nome do produto
            var producao = _context.Maquinas
                .Include(m => m.Produto)
                .GroupBy(m => m.Produto.Nome)
                .Select(g => new { nome = g.Key, qtd = g.Count() })
                .OrderByDescending(x => x.qtd)
                .Take(5)
                .ToList();

            // 2. Status das Máquinas (Vendida vs Estoque)
            var statusMaquinas = _context.Maquinas
                .GroupBy(m => m.Status)
                .Select(g => new { status = g.Key, qtd = g.Count() })
                .ToList();

            // 3. Movimentação Recente
            var movs = _context.MovimentoEstoque
                .OrderByDescending(m => m.Data)
                .Take(10) // Pega os ultimos 10 movimentos para o grafico
                .GroupBy(m => m.Tipo)
                .Select(g => new { tipo = g.Key, qtd = g.Sum(x => x.Quantidade) })
                .ToList();

            return Ok(new { producao, maquinas = statusMaquinas, movimentacao = movs });
        }

        // ==========================================
        // GERAL: Clientes, Canais, Vendas
        // ==========================================
        [HttpGet("geral")]
        public IActionResult GetGeral()
        {
            // 1. Tipo de Cliente (PF vs PJ) - Tabela Usuario
            var clientes = _context.Usuarios
                .GroupBy(u => u.Tipo)
                .Select(g => new { 
                    tipo = g.Key == "consumidor" ? "Pessoa Física" : "Empresa", 
                    qtd = g.Count() 
                })
                .ToList();

            // 2. Canal de Venda (Online vs Presencial) - Tabela Pedidos
            var canais = _context.Pedidos
                .GroupBy(p => p.TipoVenda)
                .Select(g => new { canal = g.Key, qtd = g.Count() })
                .ToList();

            // 3. Peças vs PC Montado (Agrupado por Categoria da Maquina se for PC, ou Tipo do Produto)
            // Aqui vamos simplificar: Agrupar por Produto.Tipo ('peca' vs 'pcMontado')
            // Mas você pediu detalhe das maquinas (fraca, media, potente).
            
            // Vamos pegar itens de pedido, ver se é PC montado, e se for, pegar a categoria da maquina associada
            // Como a tabela produtos tem 'pcMontado', vamos usar o nome do produto que já diz (Ex: "PC Gamer Fraco")
            var tipoVenda = _context.ItensPedido
                .Include(i => i.Produto)
                .GroupBy(i => i.Produto.Nome) // Agrupa por nome (ex: Processador, PC Fraco)
                .Select(g => new { cat = g.Key, qtd = g.Sum(i => i.Quantidade) })
                .OrderByDescending(x => x.qtd)
                .Take(5) // Top 5 categorias vendidas
                .ToList();

            // 4. Top Itens (Geral)
            var topItens = _context.ItensPedido
                .Include(i => i.Produto)
                .GroupBy(i => i.Produto.Nome)
                .Select(g => new { nome = g.Key, total = g.Sum(i => i.Quantidade) })
                .OrderByDescending(x => x.total)
                .Take(3)
                .ToList();

            return Ok(new { clientes, canais, tipoVenda, topItens });
        }

        // ==========================================
        // LOGÍSTICA: Fornecedores
        // ==========================================
        [HttpGet("logistica")]
        public IActionResult GetLogistica()
        {
            // 1. Fornecedores mais contratados (Quem tem mais pedidos de compra)
            var topFornecedores = _context.ComprasFornecedores
                .Include(c => c.Fornecedor)
                .GroupBy(c => c.Fornecedor.Nome)
                .Select(g => new { nome = g.Key, contratos = g.Count() })
                .OrderByDescending(x => x.contratos)
                .ToList();

            // 2. O que a TechFuture mais compra (Itens de Compra)
            var materiais = _context.ItensCompra
                .Include(i => i.Produto)
                .GroupBy(i => i.Produto.Nome)
                .Select(g => new { material = g.Key, qtd = g.Sum(i => i.Quantidade) })
                .OrderByDescending(x => x.qtd)
                .Take(5)
                .ToList();

            return Ok(new { topFornecedores, materiaisComprados = materiais });
        }
    }
}