using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabalhoCantina.Models
{
    public class Venda
    {
        [Key]
        public int VendaId { get; set; }

        [Required]
        public DateTime DataVenda { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente cliente { get; set; } // Chave estrangeira para o cliente

        [ForeignKey("ProdutoId")]
        public Produto produto {get; set; }

        [Required]
        public double PrecoUnitario {get; set; }


        // public ICollection<ItemVenda> ItensVenda { get; set; } = new List<ItemVenda>(); // Inicializa a coleção

        // Outras propriedades, como total da venda, desconto, etc., podem ser adicionadas conforme necessário

        // Construtor padrão sem parâmetros necessário para o EF Core
        public Venda()
        {
        }
        
    }
}