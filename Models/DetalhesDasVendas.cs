using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabalhoCantina.Models{
public class DetalheVenda
{
    // Propriedades da classe DetalheVenda

    [Key]
    public int DetalheVendaId { get; set; }

    [Required]
    public int ProdutoId { get; set; } // Chave estrangeira para o produto

    [Required]
    public int Quantidade { get; set; }

    [Required]
    public decimal PrecoUnitario { get; set; }

    [Required]
    public int VendaId { get; set; } // Chave estrangeira para Venda

    // Propriedade de navegação para a Venda à qual este detalhe pertence
    [ForeignKey("VendaId")]
    public Venda venda { get; set; }
}
}