using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TrabalhoCantina.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }

        [Required] // Defina outras anotações de validação, se necessário
        public string CPF { get; set; }
        public string Cep {get; set;}
        public string Endereco {get; set;}
        public string Telefone { get; set; }
        public string Email { get; set; }

        // Construtor padrão sem parâmetros necessário para o EF Core
        public Cliente()
        {
        }

        // Construtor para inicializar todas as propriedades não anuláveis
        public Cliente(string nome, DateTime dataNascimento, string cpf, string telefone, string email)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            CPF = cpf;
            Telefone = telefone;
            Email = email;
        }
    }
}
