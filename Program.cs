using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrabalhoCantina.Models;
using TrabalhoCantina.Data;
using Microsoft.AspNetCore.Http;
using TrabalhoCantina.Controller;
using TrabalhoCantina.Controllers;





namespace TrabalhoCantina
{
	class Program
	{
		static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			
			var connectionString = builder.Configuration.GetConnectionString("TrabalhoCantinaDB") ?? "Server=127.0.0.1;Port=3306;Database=TrabalhoCantinaDB;User=root;Password=Edu.6232;";
			var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            builder.Services.AddDbContext<CantinaDbContext>(options =>
            {
                options.UseMySql(connectionString, serverVersion, mySqlOptions => mySqlOptions.EnableRetryOnFailure());
            });
			
			var app = builder.Build();

		

		
		// Listar todos os clientes
app.MapGet("/clientes", (CantinaDbContext dbContext) => {
    return dbContext.Clientes.ToList();
});

// Listar cliente por ID
app.MapGet("/cliente/{id}", (CantinaDbContext dbContext, int id) => {
    return dbContext.Clientes.Find(id);
});

// Listar clientes por CPF
app.MapGet("/clienteCpf/{Cpf}", (CantinaDbContext dbContext, string Cpf) => {	
    return dbContext.Clientes.FirstOrDefault(cliente => cliente.CPF.Replace(".", "").Replace("-", "") == Cpf.Replace(".", "").Replace("-", ""));
});



// Listar clientes por nome
app.MapGet("/clienteNome/{nome}", (CantinaDbContext dbContext, string nome) => {
    return dbContext.Clientes.Where(cliente => EF.Functions.Like(cliente.Nome, $"%{nome}%")).ToList();
});

// Cadastrar cliente
app.MapPost("/api/cadastrarCliente", (CantinaDbContext dbContext, Cliente cliente) => {
    string resp = ClienteControl.ValidarCadastroCliente(cliente);
    if (ClienteControl.ValidarCadastroCliente(cliente) == "valido"){
        try
        {
            dbContext.Clientes.Add(ClienteControl.AddDadosCliente(cliente));
            dbContext.SaveChanges();
            resp = "Cliente adicionado";
        }
        catch (System.Exception erro)
        {
            resp = $"Ocorreu uma exceção: {erro.Message}";
        }
    }
    else{
        resp = ClienteControl.ValidarCadastroCliente(cliente);
    }
    return resp;
});

// Atualizar cliente por ID
app.MapPut("/atualizarCliente/{id}", (CantinaDbContext dbContext, Cliente clienteAtualizado, int id) => {
    var cliente = dbContext.Clientes.Find(id);
    if (cliente != null)
    {
        cliente.Nome = clienteAtualizado.Nome;
        cliente.Telefone = clienteAtualizado.Telefone;
        cliente.Email = clienteAtualizado.Email;
        cliente.Endereco = clienteAtualizado.Endereco;
        cliente.Cep = clienteAtualizado.Cep;
        dbContext.SaveChanges();
        return "Cliente atualizado";
    }
    else
    {
        return "Cliente não encontrado";
    }
});

// Deletar cliente por ID
app.MapDelete("/deletarCliente/{id}", (CantinaDbContext dbContext, int id) => {
    var cliente = dbContext.Clientes.Find(id);
    if (cliente != null)
    {
        dbContext.Remove<Cliente>(cliente);
        dbContext.SaveChanges();
        return "Cliente deletado";
    }
    else
    {
        return "Cliente não encontrado";
    }
});



//=---------------------------------------------------------------------------------------------------------------------------------------------------------------
		
// Listar todos os produtos
app.MapGet("/produtos", (CantinaDbContext dbContext) => {
    var produtoController = new ProdutoController(dbContext);
    return produtoController.ObterTodosProdutos();
});

// Listar produto por ID
app.MapGet("/produto/{id}", (CantinaDbContext dbContext, int id) => {
    var produtoController = new ProdutoController(dbContext);
    return produtoController.ObterProdutoPorID(id);
});

// Cadastrar produto
app.MapPost("/api/cadastrarProduto", (CantinaDbContext dbContext, Produto produto) => {
    var produtoController = new ProdutoController(dbContext);
    return produtoController.CadastrarProduto(produto);
});

// Atualizar produto por ID
app.MapPut("/atualizarProduto/{id}", (CantinaDbContext dbContext, Produto produtoAtualizado, int id) => {
    var produtoController = new ProdutoController(dbContext);
    var sucesso = produtoController.AtualizarProduto(id, produtoAtualizado);
    if (sucesso)
    {
        return "Produto atualizado";
    }
    else
    {
        return "Produto não encontrado";
    }
});

// Deletar produto por ID
app.MapDelete("/deletarProduto/{id}", (CantinaDbContext dbContext, int id) => {
    var produtoController = new ProdutoController(dbContext);
    var sucesso = produtoController.DeletarProduto(id);
    if (sucesso)
    {
        return "Produto deletado";
    }
    else
    {
        return "Produto não encontrado";
    }
});

// Listar todas as vendas
app.MapGet("/vendas", (CantinaDbContext dbContext) =>
{
    var vendaController = new VendasController(dbContext); // Usando VendasController
    var vendas = vendaController.GetAll();
    return vendas;
});

// Listar venda por ID
app.MapGet("/venda/{id}", (CantinaDbContext dbContext, int id) =>
{
    var vendaController = new VendasController(dbContext); // Usando VendasController
    var venda = vendaController.GetById(id);
    return venda;
});

// Cadastrar venda
app.MapPost("/api/cadastrarVenda", (CantinaDbContext dbContext, Venda venda) =>
{
    var vendaController = new VendasController(dbContext); // Usando VendasController
    var resultado = vendaController.Create(venda);
    return resultado;
});

// Atualizar venda por ID
app.MapPut("/atualizarVenda/{id}", (CantinaDbContext dbContext, Venda vendaAtualizada, int id) =>
{
    var vendaController = new VendasController(dbContext); // Usando VendasController
    var resultado = vendaController.Update(id, vendaAtualizada);
    return resultado;
});

// Deletar venda por ID
app.MapDelete("/deletarVenda/{id}", (CantinaDbContext dbContext, int id) =>
{
    var vendaController = new VendasController(dbContext); // Usando VendasController
    var resultado = vendaController.Delete(id);
    return resultado;
});

app.Run();


		}
	}
}			



