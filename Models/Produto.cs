using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TrabalhoCantina.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public decimal Preco { get; set; }

        public int Estoque { get; set; }

        // Construtor padrão sem parâmetros necessário para o EF Core
        public Produto()
        {
        }

        // Construtor para inicializar todas as propriedades não anuláveis
        public Produto(string nome, string descricao, decimal preco, int estoque)
        {
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Estoque = estoque;
        }
    }
}
