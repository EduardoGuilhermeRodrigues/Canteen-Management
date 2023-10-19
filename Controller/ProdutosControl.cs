using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TrabalhoCantina.Data;
using TrabalhoCantina.Models;

namespace TrabalhoCantina.Controllers
{
    public class ProdutoController
    {
        private readonly CantinaDbContext _context;

        public ProdutoController(CantinaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Produto> ObterTodosProdutos()
        {
            return _context.Produtos.ToList();
        }

        public Produto ObterProdutoPorID(int id)
        {
            return _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        }

        public Produto CadastrarProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return produto;
        }

        public bool AtualizarProduto(int id, Produto produtoAtualizado)
        {
            var produtoExistente = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if (produtoExistente != null)
            {
                produtoExistente.Nome = produtoAtualizado.Nome;
                produtoExistente.Descricao = produtoAtualizado.Descricao;
                produtoExistente.Preco = produtoAtualizado.Preco;
                produtoExistente.Estoque = produtoAtualizado.Estoque;
                _context.SaveChanges();
                return true; // Indica que a atualização foi bem-sucedida
            }

            return false; // Indica que o produto não foi encontrado
        }

        public bool DeletarProduto(int id)
        {
            var produtoExistente = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if (produtoExistente != null)
            {
                _context.Produtos.Remove(produtoExistente);
                _context.SaveChanges();
                return true; // Indica que o produto foi encontrado e removido
            }

            return false; // Indica que o produto não foi encontrado
        }
    }
}
