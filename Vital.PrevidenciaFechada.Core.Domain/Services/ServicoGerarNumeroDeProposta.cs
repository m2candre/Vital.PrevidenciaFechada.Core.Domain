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
		/// Construtor do serviço de domínio injetando o repositório de proposta
		/// </summary>
		public ServicoGerarNumeroDeProposta(IRepositorioProposta repositorio)
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
		/// Gera um novo número sequencial para a proposta
		/// </summary>
		/// <returns></returns>
		public virtual string GerarNumeroDeProposta()
		{
			#region Pré-condições

			IAssertion oRepositorioFoiInformado = Assertion.NotNull(_repositorio, "O repositório não foi injetado corretamente");

			#endregion

			oRepositorioFoiInformado.Validate();

			string numeroGerado = (_repositorio.ObterUltimoNumeroDaProposta() + 1).ToString();

			#region Pós-condições

			IAssertion oNumeroGeradoEMaiorQueZero = Assertion.GreaterThan(Convert.ToInt32(numeroGerado), 0, "O número gerado deve ser maior que zero");

			#endregion

			oNumeroGeradoEMaiorQueZero.Validate();

			return numeroGerado;
		}
	}
}