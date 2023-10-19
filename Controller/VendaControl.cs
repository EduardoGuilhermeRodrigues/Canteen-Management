using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TrabalhoCantina.Models;
using TrabalhoCantina.Data;
using teste.Migrations;

namespace TrabalhoCantina.Controllers
{
    [Route("api/vendas")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly CantinaDbContext _dbContext;

        public VendasController(CantinaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Create(Venda venda)
        {
            try
            {
                // Verifique se o cliente com o ID especificado existe no banco de dados
                var cliente = _dbContext.Clientes.FirstOrDefault(c => c.ClienteId == venda.cliente.ClienteId);
                if (cliente == null)
                {
                    return BadRequest("Cliente não encontrado");
                }

                // Certifique-se de configurar a data da venda
                venda.DataVenda = DateTime.Now;

                // Adicione a venda ao contexto do Entity Framework
                _dbContext.Vendas.Add(venda);

                // Salve as alterações no banco de dados
                _dbContext.SaveChanges();

                // Retorna a venda criada
                return CreatedAtAction(nameof(GetById), new { id = venda.VendaId }, venda);
            }
            catch (Exception ex)
            {
                // Lida com exceções e retorna uma resposta de erro
                return BadRequest($"Falha ao criar a venda: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var vendas = _dbContext.Vendas.ToList();
            return Ok(vendas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var venda = _dbContext.Vendas.FirstOrDefault(v => v.VendaId == id);
            if (venda == null)
            {
                return NotFound();
            }
            return Ok(venda);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Venda venda)
        {
            var vendaExistente = _dbContext.Vendas.FirstOrDefault(v => v.VendaId == id);
            if (vendaExistente == null)
            {
                return NotFound();
            }

            try
            {
                // Atualize os campos relevantes da vendaExistente com os valores de venda
                vendaExistente.DataVenda = venda.DataVenda;
                vendaExistente.cliente = venda.cliente;

                // Atualize a venda no contexto do Entity Framework
                _dbContext.Vendas.Update(vendaExistente);

                // Salve as alterações no banco de dados
                _dbContext.SaveChanges();

                return Ok(vendaExistente);
            }
            catch (Exception ex)
            {
                // Lida com exceções e retorna uma resposta de erro
                return BadRequest($"Falha ao atualizar a venda: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var venda = _dbContext.Vendas.FirstOrDefault(v => v.VendaId == id);
            if (venda == null)
            {
                return NotFound();
            }

            try
            {
                // Remova a venda do contexto do Entity Framework
                _dbContext.Vendas.Remove(venda);

                // Salve as alterações no banco de dados
                _dbContext.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Lida com exceções e retorna uma resposta de erro
                return BadRequest($"Falha ao excluir a venda: {ex.Message}");
            }
        }
    }
}
