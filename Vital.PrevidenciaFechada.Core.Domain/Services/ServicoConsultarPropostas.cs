using System;
using System.Collections.Generic;
using System.Linq;
using Vital.Extensions.DateTimeExtensions;
using Vital.InfraStructure.DSL.DesignByContract;
using Vital.PrevidenciaFechada.Core.Domain.Entities.ComponenteProposta;
using Vital.PrevidenciaFechada.Core.Domain.Repository;
using Vital.PrevidenciaFechada.DTO.Messages.Core;

namespace Vital.PrevidenciaFechada.Core.Domain.Services
{
	/// <summary>
	/// Serviço de consulta de propostas por critérios de Plano, Estado e Data
	/// </summary>
	public class ServicoConsultarPropostas
	{
		private IRepositorio<Proposta> _repositorio;
		private ConsultaDTO _consultaDTO;
		private CriteriosDeConsultaPorPlanoEstadoData _criteriosConsulta;
		private DateTime _data;

		/// <summary>
		/// Obtém data atual, se data não estiver definida
		/// </summary>
		public DateTime Data
		{
			get
			{
				if (_data == default(DateTime))
					_data = DateTime.Now;
				return _data;
			}
			set { _data = value; }
		}

		/// <summary>
		/// Obtém a classe de criação dos critérios para a consulta
		/// </summary>
		public CriteriosDeConsultaPorPlanoEstadoData CriteriosConsulta
		{
			get
			{
				if (_criteriosConsulta == null)
					_criteriosConsulta = new CriteriosDeConsultaPorPlanoEstadoData();

				return _criteriosConsulta;
			}
			set { _criteriosConsulta = value; }
		}

		/// <summary>
		/// Construtor do serviço de domínio injetando o repositório de proposta
		/// </summary>
		public ServicoConsultarPropostas(IRepositorio<Proposta> repositorio, ConsultaDTO consultaDTO)
		{
			#region Pré-condições

			IAssertion oRepositorioFoiInformado = Assertion.NotNull(repositorio, "O repositório não foi injetado corretamente");
			IAssertion aConsultaDTOFoiInformada = Assertion.NotNull(consultaDTO, "O DTO de consulta não foi informado corretamente");

			#endregion

			oRepositorioFoiInformado.and(aConsultaDTOFoiInformada).Validate();

			_repositorio = repositorio;
			_consultaDTO = consultaDTO;

			#region Pós-condições

			IAssertion oRepositorioFoiInjetadoCoretamente = Assertion.Equals(_repositorio, repositorio, "O repositório da classe não está igual ao repositório injetado");
			IAssertion aConsultaFoiAtribuidaCorretamente = Assertion.Equals(_consultaDTO, consultaDTO, "O DTO de consulta da classe não está igual ao DTO informado");

			#endregion

			oRepositorioFoiInjetadoCoretamente.and(aConsultaFoiAtribuidaCorretamente).Validate();
		}

		/// <summary>
		/// Realiza consulta para obter as propostas pelo estado e período, informado em dias
		/// </summary>
		/// <param name="idDoPlano">ID do plano do contexto</param>
		/// <param name="estado">Estado das propostas</param>
		/// <param name="quantidadeDeDias">Período em quantidade dias</param>
		/// <returns></returns>
		public IList<Proposta> ObterPropostasPorPlanoEstadoEPeriodo(Guid idDoPlano, string estado, int quantidadeDeDias)
		{
			#region Pré-condições

			IAssertion oRepositorioFoiInjetadoNoServico = Assertion.NotNull(_repositorio, "O repositório não foi injetado corretamente");
			IAssertion oDTODeConsultaFoiInformado = Assertion.NotNull(_consultaDTO, "O DTO de consulta não foi informado corretamente");
			IAssertion oIDDoPlanoFoiInformado = Assertion.IsTrue(idDoPlano != Guid.Empty, "O ID do plano deve ser informado");
			IAssertion oEstadoFoiInformado = Assertion.IsFalse(string.IsNullOrWhiteSpace(estado), "O estado da proposta deve ser informado");
			IAssertion aQuantidadeDeDiasFoiInformada = Assertion.GreaterThan(quantidadeDeDias, default(int), "A quantidade de dias deve ser maior que 0");

			#endregion

			oRepositorioFoiInjetadoNoServico.and(oDTODeConsultaFoiInformado).and(oIDDoPlanoFoiInformado).and(oEstadoFoiInformado).and(aQuantidadeDeDiasFoiInformada).Validate();

			DateTime dataDaBusca = Data.SubtrairDias(quantidadeDeDias);
			var criterios = CriteriosConsulta.ObterCriterio(idDoPlano, estado, dataDaBusca);

			var propostasEncontradas = _repositorio.ObterTodosFiltradosComCriterio<Proposta>(criterios, _consultaDTO);

			#region Pós-condições

			Assertion.NotNull(propostasEncontradas, "A lista de propostas encontradas não pode ser nula").Validate();

			#endregion

			return propostasEncontradas.ToList();
		}
	}
}