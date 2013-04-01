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
		private IRepositorio<Proposta> _repositorio;

		/// <summary>
		/// Construtor do serviço de domínio injetando o repositório de proposta
		/// </summary>
		public ServicoGerarNumeroDeProposta(IRepositorio<Proposta> repositorio)
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
			return (ObterUltimoNumeroGerado() + 1).ToString();
		}

		/// <summary>
		/// Obtém o último número sequencial de proposta gerado
		/// </summary>
		/// <returns></returns>
		private int ObterUltimoNumeroGerado()
		{
			#region Pré-condições

			IAssertion oRepositorioFoiInjetadoNoServico = Assertion.NotNull(_repositorio, "O repositório não foi injetado corretamente");

			#endregion

			oRepositorioFoiInjetadoNoServico.Validate();

			int numeroGerado = default(int);
			var propostas = _repositorio.Todas();

			if (propostas.Count > 0)
				numeroGerado = ObterMaiorNumeroDeProposta(propostas);

			return numeroGerado;
		}

		/// <summary>
		/// Obtém o maior número entre as propostas existentes
		/// </summary>
		/// <param name="propostas"></param>
		/// <returns></returns>
		private int ObterMaiorNumeroDeProposta(IList<Proposta> propostas)
		{
			return propostas.Max(p => Convert.ToInt32(p.Numero));
		}
	}
}