using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TrabalhoCantina.Data;
using TrabalhoCantina.Models;

namespace TrabalhoCantina.Controller{
     public partial class ClienteControl{

        public static string ValidarCadastroCliente(Cliente cliente){
            string resp;
            if(Validaçao.ValidarCpf(cliente.CPF) &&
				Validaçao.ValidarTelefone(cliente.Telefone) && 
				Validaçao.ValidarEmail(cliente.Email) && 
				Validaçao.ValidarCep(cliente.Cep))
				{
						resp = "valido";
				}
//Retorno invalido cpf=====================================================================================================================================================================
				else if(!Validaçao.ValidarCpf(cliente.CPF)){
					if(!Validaçao.ValidarTelefone(cliente.Telefone)){
						if(!Validaçao.ValidarEmail(cliente.Email)){
							if(!Validaçao.ValidarCep(cliente.Cep)){
								resp = "cpf, telefone, email, cep invalido";
							}
							else{
								resp = "cpf, telefone, email invalido";
							}
						}
						else if(!Validaçao.ValidarCep(cliente.Cep)){
							resp = "cpf, telefone, cep invalido";
						}
						else{
							resp = "cpf, telefone invalido";
						}
					}
					else if(!Validaçao.ValidarEmail(cliente.Email)){		
						if(!Validaçao.ValidarCep(cliente.Cep)){
							resp = " cpf, email, cep invalido";
						}
						else{
							resp = " cpf, email invalido";
						}
					}
					else if(!Validaçao.ValidarCep(cliente.Cep)){
						resp = "cpf, cep invalido";
					}
					else{
						resp = "cpf invalido";
					}
				}
/*retorno telefone invalido====================================================================================================*/
				else if(!Validaçao.ValidarTelefone(cliente.Telefone)){
					if(!Validaçao.ValidarEmail(cliente.Email)){
						if(!Validaçao.ValidarCep(cliente.Cep)){
							resp = "telefone, email, cep invalido";
						}
						else{
							resp = " telefone, email invalido";
						}
					}
					else if(!Validaçao.ValidarCep(cliente.Cep)){
						resp = "telefone, cep invalido";
					}
					else{
						resp = "telefone invalido";
					}
				}
/*retorno Email invalido=========================================================================================*/		
				else if(!Validaçao.ValidarEmail(cliente.Email)){
					if(!Validaçao.ValidarCep(cliente.Cep)){
						resp = "email, cep invalido";
					}
					else{
						resp = "email invalido";
					}
				}
/*retorno CEP invalido==========================================================================================*/		
				else if(!Validaçao.ValidarCep(cliente.Cep)){
					resp = "cep invalido";
				}
				else {
					resp = "erro desconhecido consulte o suporte tecnico";
				}
			return resp;
        }

		public static Cliente AddDadosCliente(Cliente cliente){
			cliente.CPF = Validaçao.PadronizarCpf(cliente.CPF);
			return cliente;
		}
    }
}