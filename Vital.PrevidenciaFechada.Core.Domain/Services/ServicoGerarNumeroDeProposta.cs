using System;
using System.Collections.Generic;
using System.Linq;
using Vital.InfraStructure.DSL.DesignByContract;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;
using Vital.PrevidenciaFechada.Core.Domain.Repository;

namespace Vital.PrevidenciaFechada.Core.Domain.Services
{
	/// <summary>
	/// Serviço de domínio para geração de número de proposta sequencial
	/// </summary>
	public class ServicoGerarNumeroDeProposta
	{
		/// <summary>
		/// Repositório de Proposta
		/// </summary>
		private IRepositorioProposta _repositorio;

		/// <summary>
		/// Controle da instância única do serviço
		/// </summary>
		private static ServicoGerarNumeroDeProposta _instanciaDoServico;
		
		/// <summary>
		/// Objecto para controle do lock
		/// </summary>
		private static readonly object lockObject = new object();

		/// <summary>
		/// Construtor privado do serviço de domínio injetando o repositório de proposta
		/// </summary>
		private ServicoGerarNumeroDeProposta(IRepositorioProposta repositorio)
		{
			#region Pré-condições

			IAssertion oRepositorioFoiInformado = Assertion.NotNull(repositorio, "O repositório não foi injetado corretamente");

			#endregion

			oRepositorioFoiInformado.Validate();

			_repositorio = repositorio;

			#region Pós-condições

			IAssertion oRepositorioFoiInjetadoCoretamente = Assertion.Equals(_repositorio, repositorio, "O repositório da classe não está igual ao repositório injetado");

			#endregion

			oRepositorioFoiInjetadoCoretamente.Validate();
		}

		/// <summary>
		/// Método estático para obter uma instância do Serviço
		/// </summary>
		/// <param name="repositorio"></param>
		/// <returns></returns>
		public static ServicoGerarNumeroDeProposta ObterServico(IRepositorioProposta repositorio)
		{
			if (_instanciaDoServico == null)
			{
				lock (lockObject)
				{
					if (_instanciaDoServico == null)
						_instanciaDoServico = new ServicoGerarNumeroDeProposta(repositorio);
				}
			}

			return _instanciaDoServico;
		}

		/// <summary>
		/// Gera um novo número sequencial e persite a proposta em rascunho com o número gerado
		/// </summary>
		/// <returns>Número gerado para a proposta</returns>
		public virtual int GerarNumeroDeProposta()
		{
			#region Pré-condições

			IAssertion oRepositorioFoiInformado = Assertion.NotNull(_repositorio, "O repositório não foi injetado corretamente");

			#endregion

			oRepositorioFoiInformado.Validate();

			int numeroGerado = _repositorio.ObterUltimoNumeroDaProposta() + 1;
			PersistirPropostaComNumeroGerado(numeroGerado);
			
			#region Pós-condições

			IAssertion oNumeroGeradoEMaiorQueZero = Assertion.GreaterThan(numeroGerado, default(int), "O número gerado deve ser maior que zero");

			#endregion

			oNumeroGeradoEMaiorQueZero.Validate();

			return numeroGerado;
		}

		/// <summary>
		/// Persiste a proposta em rascunho com o número gerado
		/// </summary>
		/// <param name="numeroGerado"></param>
		private void PersistirPropostaComNumeroGerado(int numeroGerado)
		{
			#region Pré-condições

			IAssertion oNumeroGeradoFoiInformado = Assertion.GreaterThan(numeroGerado, default(int), "O número gerado deve ser informado");

			#endregion

			oNumeroGeradoFoiInformado.Validate();

			Proposta proposta = new Proposta { Numero = numeroGerado };
			_repositorio.Adicionar(proposta);

			#region Pós-condições

			IAssertion oNumeroDaPropostaFoiDefinidoNaEntidade = Assertion.GreaterThan(proposta.Numero, default(int), "O número da proposta não foi definido na entidade");

			#endregion

			oNumeroDaPropostaFoiDefinidoNaEntidade.Validate();
		}
	}
}